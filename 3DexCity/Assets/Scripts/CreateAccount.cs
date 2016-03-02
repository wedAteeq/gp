using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using UnityEngine.UI;
using Sfs2X.Util;

public class CreateAccount : MonoBehaviour
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
    private string password;
    private string Conpassword;
    private string email;
    private string biography;
    private string firstname;
    private string lastname;
    private string Act_Room;
    private string Account_T;
    private string Avt;
    private int Error = 0;
    private int room = 0;


    //----------------------------------------------------------
    // UI elements
    //----------------------------------------------------------
    public InputField UserName;
    public InputField Password;
    public InputField ConPassword;
    public Text TextMessage;
    public InputField Email;
    public InputField FirstName;
    public InputField LastName;
    public InputField Biography;
    public Toggle ActivateRoom;
    public Toggle AccountType;
    public Toggle FAvatar;
    public Toggle MAvatar;
    public Transform Result;
    public Transform createAccount;

    string CMD_Signup = "$SignUp.Submit";


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


    public void OnCreateAccountButtonClicked()
    {
        int usernameSpace, firstnameSpace, lastnameSpace;
        Error = 0;
        username = UserName.text;
        usernameSpace = username.IndexOf(" ");
        password = Password.text;
        Conpassword = ConPassword.text;
        email = Email.text;
        biography = Biography.text;
        firstname = FirstName.text;
        firstnameSpace = firstname.IndexOf(" ");
        lastname = LastName.text;
        lastnameSpace = lastname.IndexOf(" ");
        bool ARoom = ActivateRoom.isOn;
        if (ARoom == true)
            Act_Room = "Y";
        else
            Act_Room = "N";

        bool Account = AccountType.isOn;
        if (Account == true)
            Account_T = "private";
        else
            Account_T = "public";

        bool avatar = MAvatar.isOn;
        if (avatar == true)
            Avt = "M";
        else
            Avt = "F";

        if (requredFilled())
        {
            if (usernameSpace == -1 && firstnameSpace == -1 && lastnameSpace == -1)
            {
                if (password == Conpassword)
                {
                    if (email.IndexOf("@") != -1)
                    {   // Enable interface
                        enableInterface(false);
   
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
                    }
                    else
                        TextMessage.text = "Invalid email account";
                }
                else
                    TextMessage.text = "The password and its confirm are not matching";
            }
            else
                TextMessage.text = "Username,firstname & lastname should not contains a space";
        }
        else
            TextMessage.text = "Missing to fill required value";

    }//end create account



    private void reset()
    {
        // Remove SFS2X listeners
        sfs.RemoveAllEventListeners();

        sfs = null;

        // Enable interface
        enableInterface(true);
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
    }

    private void OnExtensionResponse(BaseEvent evt)
    {
        string cmd = (string)evt.Params["cmd"];
        ISFSObject objIn = (SFSObject)evt.Params["params"];

        string message;
        if (cmd == CMD_Signup)
        {

            if (objIn.ContainsKey("success"))
            {
                message = "Signup Successful";
                Debug.Log(message);
                TextMessage.text = message;

            }
            else
            {
                Error = 1;
                message = objIn.GetUtfString("errorMessage");
                message = "Signup Error: " + message;
                Debug.Log(message);
                TextMessage.text = message;
                reset();
            }
        }
        else
        if(room==1)
        {
            string result = objIn.GetUtfString("CreateRoomResult");

            if (result == "Successful")
                Debug.Log("Successful");
            else
                Debug.Log("error");
            room++;
        }

    }

    private void OnLoginError(BaseEvent evt)
    {
        // Disconnect
        sfs.Disconnect();

        // Remove SFS2X listeners and re-enable interface
        reset();

        // Show error message
        TextMessage.text = "Login failed: " + (string)evt.Params["errorMessage"];
        Debug.Log(TextMessage.text);
    }

    private void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
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
        objOut.PutUtfString("password", password);
        objOut.PutUtfString("email", email);
        objOut.PutUtfString("firstName", firstname);
        objOut.PutUtfString("lastName", lastname);
        objOut.PutUtfString("biography", biography);
        objOut.PutUtfString("hasRoom", Act_Room);
        objOut.PutUtfString("accountType", Account_T);
        objOut.PutUtfString("avatar", Avt);
  
        sfs.Send(new ExtensionRequest(CMD_Signup, objOut));

        if (Act_Room=="Y")
        {
            Debug.Log("1");
            room = 1;
            SFSObject objOut2 = new SFSObject();
            objOut2.PutUtfString("username", username);
            objOut2.PutUtfString("accountType", Account_T);
            sfs.Send(new ExtensionRequest("CreateRoom", objOut2));
        }

        if (Error == 0)
        {
            createAccount.gameObject.SetActive(false);
            Result.gameObject.SetActive(true);
        }

    }

    private void enableInterface(bool enable)
    {
        UserName.interactable = enable;
        Password.interactable = enable;
        ConPassword.interactable = enable;
        Email.interactable = enable;
        FirstName.interactable = enable;
        LastName.interactable = enable;
        ActivateRoom.interactable = enable;
        AccountType.interactable = enable;

        TextMessage.text = "";
    }

    private bool requredFilled()
    {
        if (username == "" || username == " " || password == "" || password == " " || Conpassword == "" || Conpassword == " " || email == "" || email == " ")
            return false;
        else
            return true;

    }

}
