using UnityEngine;
using UnityEngine.UI;
using Sfs2X;
using Sfs2X.Util;
using Sfs2X.Core;
using Sfs2X.Requests;
using System;
using Sfs2X.Entities.Data;

public class manageMemberAccount : MonoBehaviour
{

    //----------------------------------------------------------
    // Private properties (Connection part)
    //----------------------------------------------------------
    private string ServerIP = "127.0.0.1";// Default host
    private int defaultTcpPort = 9933;// Default TCP port
    private int defaultWsPort = 8888;			// Default WebSocket port
    private string ZoneName = "3DexCityZone";
    private int ServerPort = 0;

    private SmartFox sfs;
    private string username;

    //----------------------------------------------------------
    // UI elements
    //----------------------------------------------------------
    public InputField UserName;
    public Text TextMessage;
    public Transform Delete, Home;
    public Transform viewPage;

    public InputField View_username;
    public InputField View_Password;
    public InputField ConPassword;
    public InputField Email;
    public InputField FirstName;
    public InputField LastName;
    public InputField Biography;
    public Toggle ActivateRoom;
    public Toggle AccountType;
    public Toggle FAvatar;
    public Toggle MAvatar;

    private int viewStatus = 0;
    private int deleteStatus=0 ;
    private int updateStatus=0 ;
    private string pass;
    //----------------------------------------------------------
    // Unity calback methods
    //----------------------------------------------------------

    // Use this for initialization
    void Start()
    {
        TextMessage.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        // As Unity is not thread safe, we process the queued up callbacks on every frame
        if (sfs != null)
            sfs.ProcessEvents();
    }

    //----------------------------------------------------------
    // Public interface methods for UI
    //----------------------------------------------------------

    public void OnViewMyAccountButtonclicked()
    {
        username =UserName.text;
        Debug.Log("view account");
        viewPage.gameObject.SetActive(true);
        viewStatus = 1;
        TextMessage.text = "";
        #if UNITY_WEBGL
            {
             sfs = new SmartFox(UseWebSocket.WS);
             ServerPort = defaultWsPort;
            }
       #else
        {
            sfs = new SmartFox();
            ServerPort = defaultTcpPort;
        }
       #endif

        sfs.ThreadSafeMode = true;
        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
            sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
            sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
            sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);

            sfs.Connect(ServerIP, ServerPort);

    }//end

    private void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            // Login
            Debug.Log("Successfully Connected!");

            sfs.Send(new LoginRequest("", "", ZoneName));
        }
        else
        {
            Debug.Log("Connection Failed!");
            // Remove SFS2X listeners and re-enable interface
            reset();

            // Show error message
            TextMessage.text = "Connection failed; is the server running at all?";
        }
    }

    private void OnLogin(BaseEvent evt)
    {
        Debug.Log("Logged In: " + evt.Params["user"]);

            ISFSObject objOut = new SFSObject();
            objOut.PutUtfString("username", username);
            sfs.Send(new ExtensionRequest("ViewAccount", objOut));

    }

    private void enableInterface(bool enable)
    {
        TextMessage.text = "";

    }

    private void OnConnectionLost(BaseEvent evt)
    {
        // Remove SFS2X listeners and re-enable interface
        reset();

        string reason = (string)evt.Params["reason"];

        if (reason != ClientDisconnectionReason.MANUAL)
        {
            // Show error message
           TextMessage.text = "Connection was lost; reason is: " + reason;
        }
    }//end

    private void OnLoginError(BaseEvent evt)
    {    // Show error message
        string message = (string)evt.Params["errorMessage"];
       TextMessage.text = "Login failed: " + message;
        Debug.Log("Login failed: " + message);
   
            // Disconnect
            sfs.Disconnect();

            // Remove SFS2X listeners and re-enable interface
            reset();    
    }

    private void OnExtensionResponse(BaseEvent evt)
    {
        Debug.Log("2: ");

        ISFSObject objIn = (SFSObject)evt.Params["params"];
       if (viewStatus == 1)
       {
        ISFSArray useraccountinfo = objIn.GetSFSArray("account");
        
        View_username.text = useraccountinfo.GetSFSObject(0).GetUtfString("username");
        Debug.Log(View_username.text);
        pass = useraccountinfo.GetSFSObject(0).GetUtfString("password");
        View_Password.text = pass;
        ConPassword.text = pass;
        Email.text = useraccountinfo.GetSFSObject(0).GetUtfString("email");
        FirstName.text = useraccountinfo.GetSFSObject(0).GetUtfString("firstName");
        if (useraccountinfo.GetSFSObject(0).GetUtfString("biography")==null)
            Biography.text = "";
        else 
            Biography.text = useraccountinfo.GetSFSObject(0).GetUtfString("biography");

        LastName.text = useraccountinfo.GetSFSObject(0).GetUtfString("lastName");

     
            if (useraccountinfo.GetSFSObject(0).GetUtfString("hasRoom").Equals("Y"))
                ActivateRoom.isOn = true;
            else
                ActivateRoom.isOn = false;

            if (useraccountinfo.GetSFSObject(0).GetUtfString("accountType").Equals("private"))
                AccountType.isOn = true;
            else
                AccountType.isOn = false;

        if (useraccountinfo.GetSFSObject(0).GetUtfString("avatar").Equals("F"))
            FAvatar.isOn = true;
        else
            MAvatar.isOn = true;

        Email.interactable = false;
        View_username.interactable = false;
         viewStatus = 0;
       }//view account
      else 
            if (deleteStatus==1)
        {
            string result = objIn.GetUtfString("DeleteResult");

            if (result == "Successful")
            {
                Debug.Log("Successful");
                TextMessage.text = "Your account deleted successfully";
                 Home.gameObject.SetActive(true);
                 Delete.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("error");
                TextMessage.text = "Your account has not been deleted";

            }
            deleteStatus = 0;
        }//delete account
        else
            if (updateStatus == 1)
        {
            string result = objIn.GetUtfString("UpdateResult");

            if (result == "Successful")
            {
                Debug.Log("Successful");
                TextMessage.text = "Your account updated successfully";

            }
            else
            {
                Debug.Log("error");
                TextMessage.text = "Your account has not been updated";

            }
            updateStatus = 0;
        }//delete account

    }//end extension

    public void OnDeleteButtonClocked()
    {
        Debug.Log("delete account");
        if (sfs != null && sfs.IsConnected)
        {
            deleteStatus=1;
            Debug.Log("1");

            ISFSObject objOut = new SFSObject();
            objOut.PutUtfString("username", username);
            sfs.Send(new ExtensionRequest("DeleteAccount", objOut));
        }
    }//delte account

    public void OnUpdateButtonClocked()
    {
        int firstnameSpace, lastnameSpace;

        string Act_Room, Avt, Account_T,password;
        if (sfs != null && sfs.IsConnected)
        {
            Debug.Log("update account");
            if (requredFilled())
            {
               firstnameSpace = FirstName.text.IndexOf(" ");
               lastnameSpace = LastName.text.IndexOf(" ");
                if (firstnameSpace == -1 && lastnameSpace == -1)
                {
                    if (View_Password.text == ConPassword.text)
                    {
                        updateStatus = 1;
                    ISFSObject objOut = new SFSObject();
                    if (ActivateRoom.isOn == true)
                        Act_Room = "Y";
                    else
                        Act_Room = "N";

                    if (AccountType.isOn == true)
                        Account_T = "private";
                    else
                        Account_T = "public";

                    if (FAvatar.isOn == true)
                        Avt = "F";
                    else
                        Avt = "M";

                    if (pass == View_Password.text)
                       password = pass;
                    else
                       password= PasswordUtil.MD5Password(View_Password.text);

                    objOut.PutUtfString("username", username);
                    objOut.PutUtfString("account", "member");
                    objOut.PutUtfString("password", password);
                    objOut.PutUtfString("firstName", FirstName.text);
                    objOut.PutUtfString("lastName", LastName.text);
                    objOut.PutUtfString("biography", Biography.text);
                    objOut.PutUtfString("hasRoom", Act_Room);
                    objOut.PutUtfString("accountType", Account_T);
                    objOut.PutUtfString("avatar", Avt);

                    sfs.Send(new ExtensionRequest("UpdateAccount", objOut));
                    }
                else
                    TextMessage.text = "The password and its confirm are not matching";
                }
                else
                    TextMessage.text = "firstname & lastname should not contains a space";
            }
            else
                TextMessage.text = "Missing to fill required value";
        }
    }

    private bool requredFilled()
    {
        if (View_Password.text == "" || View_Password.text == " " || ConPassword.text == "" || ConPassword.text == " ")
            return false;
        else
            return true;
    }

    private void reset()
    {
        // Remove SFS2X listeners
        sfs.RemoveAllEventListeners();

        sfs = null;

        // Enable interface
        enableInterface(true);
    }

    public void OnCancelButtonClickedt()
    {
        if (sfs != null && sfs.IsConnected)
        {
            sfs.Disconnect();
            viewPage.gameObject.SetActive(false);

        }
    }
}