using UnityEngine;
using UnityEngine.UI;
using Sfs2X;
using Sfs2X.Util;
using Sfs2X.Core;
using Sfs2X.Requests;
using UnityEditor;
using Sfs2X.Entities.Data;

public class LogInPage : MonoBehaviour
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
    private int retAvatar = 0;
    private int retRooms = 0;

    //----------------------------------------------------------
    // UI elements
    //----------------------------------------------------------
    public InputField UserName;
    public InputField Password;
    public Text TextMessage;
    public Transform ActivateAccount;
    public Transform Login;
    public Transform MemberView;
    public Transform AdminView;
    public Transform Home;
    public Transform logount;
    public Animator BoyAvatar;
    public Animator GirlAvatar;

    // string CMD_ActivateAccount="$SignUp.Activate";

    //----------------------------------------------------------
    // Unity calback methods
    //----------------------------------------------------------

    // Use this for initialization
    void Start()
    {
<<<<<<< HEAD
        //BoyAvatar = GetComponent<Animator>();
        //GirlAvatar = GetComponent<Animator>();
        //BoyAvatar.gameObject.SetActive(false);
        //GirlAvatar.gameObject.SetActive(false);
=======
 
>>>>>>> origin/change

        enableInterface(true);
        TextMessage.text = "";
        UserName.text = "";
        Password.text = "";
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

    public void OnLoginButtonClicked()
    {
        username = UserName.text;
        password = Password.text;

        if (username == "" || username == " " || password == "" || password == " ")
            TextMessage.text = "Missing to fill required value";
        else
        {  // Enable interface
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

    }//end




    public void OnLogOutButtonClicked()
    {
        // Disconnect from server
        if (sfs != null && sfs.IsConnected)
        {
            sfs.Disconnect();

        }
        logount.gameObject.SetActive(false);
        Home.gameObject.SetActive(true);
        AdminView.gameObject.SetActive(false);
        MemberView.gameObject.SetActive(false);
    }

    private void OnExtensionResponse(BaseEvent evt)
    {
        string avt;
        ISFSObject objIn = (SFSObject)evt.Params["params"];
        if (retAvatar == 1)
        {

            ISFSArray useraccountinfo = objIn.GetSFSArray("Avtar");
            avt = useraccountinfo.GetSFSObject(0).GetUtfString("avatar");

            if (avt == "M" || avt == "A")
                BoyAvatar.gameObject.SetActive( true);
            else
                GirlAvatar.gameObject.SetActive(true);

            Debug.Log("Avatar: "+avt);
            retAvatar = 0;
        }
     else if (retRooms == 1)
        {
            ISFSArray Rooms = objIn.GetSFSArray("Rooms");
            Debug.Log("Room 0: " + Rooms.GetSFSObject(0).GetUtfString("username"));
            int length = Rooms.Size();
            Room [] room = new Room[length];
            for (int i=0;i< length; i++)
            {
                room[i] = new Room(int.Parse(Rooms.GetSFSObject(i).GetUtfString("Room_ID")), Rooms.GetSFSObject(i).GetUtfString("username"),Rooms.GetSFSObject(i).GetUtfString("type"));
                if (Rooms.GetSFSObject(i).GetUtfString("username") == username)
                {
                    Transverser.MyRoomID = Rooms.GetSFSObject(i).GetUtfString("Room_ID");
<<<<<<< HEAD
                    EditorUtility.DisplayDialog("Waring Message", "Your room Id is " + Transverser.MyRoomID, "ok");
=======
                     EditorUtility.DisplayDialog("Message", "Your room Id is " + Transverser.MyRoomID, "ok");
>>>>>>> origin/change
                }
            }

            Transverser.Rooms = room;
            retRooms = 0;
        }
    }

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
            Debug.Log("Connection was lost; reason is: " + reason);
        }
    }//end

    private void OnLoginError(BaseEvent evt)
    {    // Show error message
        string message = (string)evt.Params["errorMessage"];
        string msg = "Login failed: " + message;
 
        Debug.Log("Login failed: " + message);
        if (message == "Your account has not been activated yet!")
        {
            TextMessage.text = "";
            UserName.text = "";
            Password.text = "";
            enableInterface(true);

            Login.gameObject.SetActive(false);
            ActivateAccount.gameObject.SetActive(true);

        }
        else
        {
            // Disconnect
            EditorUtility.DisplayDialog("Waring Message", "         Wrong password / username", "ok");
            sfs.Disconnect();

            // Remove SFS2X listeners and re-enable interface
            reset();
        }
    }


    private void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            // Login
            Debug.Log("Successfully Connected!");

            password = PasswordUtil.MD5Password(password);//to incrypt the password
            sfs.Send(new LoginRequest(username, password, ZoneName));
        }
        else
        {
            Debug.Log("Connection Failed!");
            // Remove SFS2X listeners and re-enable interface
            reset();

            // Show error message
            TextMessage.text = "Connection Failed!";
        }
    }

    private void OnLogin(BaseEvent evt)
    {
        Debug.Log("Logged In: " + evt.Params["user"]);
        int AdminIndex = username.IndexOf("n");
        string admin = username.Substring(0, AdminIndex + 1);
        if (admin.Equals("Admin"))
            AdminView.gameObject.SetActive(true);
        else
            MemberView.gameObject.SetActive(true);

        Login.gameObject.SetActive(false);

        AAvatar avt = new AAvatar();
        avt.getAvatarTye(sfs, username);
        retAvatar++;

        Room room = new Room();
        room.getAllRooms(sfs);
        retRooms++;

    }

    private void enableInterface(bool enable)
    {
        UserName.interactable = enable;
        Password.interactable = enable;
        TextMessage.text = "";

    }


}
