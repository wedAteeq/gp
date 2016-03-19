﻿using UnityEngine;
using System.Collections;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using Sfs2X.Logging;
using UnityEngine.UI;
using System;
using Sfs2X.Util;

public class SendAcctivationEmail : MonoBehaviour
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
    private string email;
    private string message;
    private int room = 0;

    //----------------------------------------------------------
    // UI elements
    //----------------------------------------------------------
    public InputField MemberUserName;
    public InputField MemberEmail;
    public Toggle MemberAccountType;
    public Toggle MemberActivateRoom;

    public InputField AdminUserName;
    public InputField AdminEmail;
    public Transform welcome;
    public Transform Login;
    public Transform AdminView;

    string CMD_ActivateEmail = "$SignUp.ResendEmail";

    // Use this for initialization
    void Start()
    {
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


    public void OnOKButtonClicked()
    {
        if (MemberUserName.text != "")
        {
            username = MemberUserName.text;
            email = MemberEmail.text;
        }
        else
        {
            username = AdminUserName.text;
            email = AdminEmail.text;
        }

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


    private void OnConnectionLost(BaseEvent evt)
    {
        // Remove SFS2X listeners and re-enable interface

        string reason = (string)evt.Params["reason"];

        if (reason != ClientDisconnectionReason.MANUAL)
        {
            // Show error message
            Debug.Log(reason);
        }
    }

    private void OnExtensionResponse(BaseEvent evt)
    {
        string cmd = (string)evt.Params["cmd"];
        ISFSObject objIn = (SFSObject)evt.Params["params"];

        if (cmd == CMD_ActivateEmail)
        {

            if (objIn.ContainsKey("success"))
            {
                message = "The activation re-email send successfyl";
                Debug.Log(message);
                if (MemberUserName.text != "")
                    Login.gameObject.SetActive(true);
                else
                    AdminView.gameObject.SetActive(true);

                welcome.gameObject.SetActive(false);
            }
            else
            {
                message = objIn.GetUtfString("errorMessage");
                message = "Resend Error: " + message;
                Debug.Log(message);

            }
        }
        else if (room == 1)
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

        // Show error message
        string message = "Login failed: " + (string)evt.Params["errorMessage"];
        Debug.Log(message);

    }

    private void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            message = "Successfully Connected!";
            Debug.Log(message);

            sfs.Send(new LoginRequest("", "", ZoneName));
        }
        else
        {
            // Remove SFS2X listeners and re-enable interface
            message = "Connection failed; is the server running at all?";
            // Show error message
            Debug.Log(message);

        }
    }

    private void OnLogin(BaseEvent evt)
    {
        string Account;
        Debug.Log("Logged In: " + evt.Params["user"]);

        ISFSObject objOut = new SFSObject();
        objOut.PutUtfString("username", username);
        objOut.PutUtfString("email", email);

        sfs.Send(new ExtensionRequest(CMD_ActivateEmail, objOut));
       
        if (MemberActivateRoom.isOn == true )
        {
            room = 1;
            Debug.Log("1");
            objOut = new SFSObject();

            if (MemberAccountType.isOn==true)
                Account = "private";
            else
                Account = "public";
            SFSObject objOut2 = new SFSObject();
            objOut2.PutUtfString("username", username);
            objOut2.PutUtfString("accountType", Account);
            sfs.Send(new ExtensionRequest("CreateRoom", objOut2));
        }
    }


}
