﻿using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using UnityEngine.UI;
using Sfs2X.Util;

public class ForgetPassword : MonoBehaviour
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

    public Transform login;
    public Transform forgetPassword;


    string CMD_RECOVER = "$SignUp.Recover";

    // Use this for initialization
    void Start()
    {
        TextMessage.text = "";
        enableInterface(true);
        UserName.text = "";
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

    public void OnResetPasswordButtonClicked()
    {
        username = UserName.text;
        if (username == "" || username == " ")
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
            enableInterface(true);
            UserName.text = "";
            TextMessage.text = "";

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
            TextMessage.text = "Connection was lost; reason is: " + reason;
        }
    }

    private void OnExtensionResponse(BaseEvent evt)
    {
        string cmd = (string)evt.Params["cmd"];
        ISFSObject objIn = (SFSObject)evt.Params["params"];
        string message = "";
        if (cmd == CMD_RECOVER)
        {
            if (objIn.ContainsKey("errorMessage"))
            {
                 message = "Password Recovery Error: " + objIn.GetUtfString("errorMessage");
                Debug.Log(message);
                TextMessage.text = message;
            }
            else
            {
                 message = "The password was sent to your email box";
                Debug.Log(message);
                TextMessage.text = message;
                forgetPassword.gameObject.SetActive(false);
                login.gameObject.SetActive(true);
            }
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

    void OnLogin(BaseEvent e)
    {
        Debug.Log("Logged In: " + e.Params["user"]);

        ISFSObject objOut = new SFSObject();

        objOut.PutUtfString("username", username);

        sfs.Send(new ExtensionRequest(CMD_RECOVER, objOut));
    
    }

    private void enableInterface(bool enable)
    {
        UserName.interactable = enable;
       
        TextMessage.text = "";
    }

    
    void OnApplicationQuit()
    {
        if (sfs != null && sfs.IsConnected)
        {
            sfs.Disconnect();
        }
    }
}
