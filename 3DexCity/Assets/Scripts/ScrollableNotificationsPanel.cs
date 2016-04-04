using UnityEngine;
using UnityEngine.UI;
using Sfs2X.Entities.Data;
using UnityEngine.EventSystems;
using Sfs2X;
using Sfs2X.Util;
using Sfs2X.Core;
using Sfs2X.Requests;

public class ScrollableNotificationsPanel : MonoBehaviour {

    // Use this for initialization
    private string ServerIP = "127.0.0.1";// Default host
    private int defaultTcpPort = 9933;// Default TCP port
    private int defaultWsPort = 8888;           // Default WebSocket port
    private string ZoneName = "3DexCityZone";
    private int ServerPort = 0;

    private SmartFox sfs;
    private ISFSArray useraccountinfo;
    private int itemCount, columnCount = 1;
    private string decision, Room_ID, username;

    public GameObject itemPrefab;
    public GameObject itemPrefab1Parent;

    public void Start()
    {
        Debug.Log("in start");
           useraccountinfo = Transverser.userinfo;

        if (useraccountinfo != null && useraccountinfo.Size() > 0)
        {
            
            itemCount = useraccountinfo.Size();

            itemPrefab = Transverser.itemPrefab1;
            itemPrefab1Parent= Transverser.itemPrefab1Parent; 

            itemPrefab.gameObject.SetActive(true);
            RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
            RectTransform containerRectTransform = itemPrefab1Parent.GetComponent<RectTransform>();

            //calculate the width and height of each child item.
            float width = containerRectTransform.rect.width / columnCount;
            float ratio = width / rowRectTransform.rect.width;
            float height = rowRectTransform.rect.height * ratio*2;
            int rowCount = itemCount / columnCount;

            if (itemCount % rowCount > 0)
                rowCount++;

            //adjust the height of the container so that it will just barely fit all its children
            float scrollHeight = height * rowCount;
            containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
            containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

            int j = 0;
            int i;
            for ( i = 0; i < itemCount + 1; i++)
            {
                //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
                if (i % columnCount == 0)
                    j++;
              //create a new item, name it, and set the parent
                GameObject newItem = Instantiate(itemPrefab) as GameObject;
                newItem.name = itemPrefab1Parent.name + " item at (" + i + "," + j + ")";
                newItem.transform.parent = itemPrefab1Parent.transform;

                //move and size the new item
                RectTransform rectTransform = newItem.GetComponent<RectTransform>();

                float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
                float y = containerRectTransform.rect.height / 2 - height * j;
                rectTransform.offsetMin = new Vector2(x, y);

                x = rectTransform.offsetMin.x + width;
                y = rectTransform.offsetMin.y + height;
                rectTransform.offsetMax = new Vector2(x, y);

                GameObject Message, AcceptButton, RejectButton;
                Text textMessage;

                if (i == 0)
                    Message = GameObject.Find("Message");
                else
                    Message = GameObject.Find("Message" + (i - 1));


                textMessage = Message.GetComponent<Text>();
                textMessage.name = "Message" + i;
                if (i < itemCount)
                {
                    textMessage.text = useraccountinfo.GetSFSObject(i).GetUtfString("name") + " requests to visit you?";
                    Debug.Log(useraccountinfo.GetSFSObject(i).GetUtfString("name"));
                }
                Button AccButton, RejButton;

                if (i == 0)
                    AcceptButton = GameObject.Find("Accept ");
                else
                    AcceptButton = GameObject.Find("Accept " + (i - 1));


                AccButton = AcceptButton.GetComponent<Button>();
                AccButton.name = "Accept " + i;

                if (i == 0)
                    RejectButton = GameObject.Find("Reject ");
                else
                    RejectButton = GameObject.Find("Reject " + (i - 1));

                RejButton = RejectButton.GetComponent<Button>();
                RejButton.name = "Reject " + i;

                if (i == 0)
                    newItem.SetActive(false);
            }

            GameObject MSG= GameObject.Find("Message" + (i-1));
            Text textMSG = MSG.GetComponent<Text>();
            textMSG.name = "Message";

            GameObject AcceptBut=GameObject.Find("Accept " + (i - 1)); 
            Button AButton = AcceptBut.GetComponent<Button>();
            AButton.name = "Accept " ;

            GameObject RejectBut=GameObject.Find("Reject " + (i - 1)); ;
            Button RButton= RejectBut.GetComponent<Button>();
            RButton.name = "Reject " ;

        }

    }

    void Update()
    {
        // As Unity is not thread safe, we process the queued up callbacks on every frame
        if (sfs != null)
            sfs.ProcessEvents();

    }
   

    public void OnDecisionButtonClicked()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int SpaceIndex = name.IndexOf(" ");
        string DecisionButtonName = name.Substring(0, SpaceIndex);
        DecisionButtonName = DecisionButtonName.ToLower();
        string DBIndex = name.Substring(SpaceIndex+1);
        int index = int.Parse(DBIndex);
        Debug.Log(" index "+ index+" decision: " + DecisionButtonName + " room " + Room_ID);
        Room_ID = Transverser.RoomID;
        decision = DecisionButtonName;
        username= Transverser.userinfo.GetSFSObject(index).GetUtfString("username");
        Debug.Log("username: " + username + " decision: " + decision+" room "+ Room_ID);

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
            Debug.Log("Connection failed; is the server running at all?");
        }
    }

    private void OnLogin(BaseEvent evt)
    {
        Debug.Log("Logged In: " + evt.Params["user"]);

        ISFSObject objOut = new SFSObject();
        objOut.PutUtfString("Room_ID", Room_ID);
        objOut.PutUtfString("username", username);
        objOut.PutUtfString("decision", decision);
        Debug.Log("username: " + username + " decision: " + decision + " room " + Room_ID);
        sfs.Send(new ExtensionRequest("Decision", objOut));
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
        Debug.Log("Login failed: " + message);

        // Disconnect
        sfs.Disconnect();

        // Remove SFS2X listeners and re-enable interface
        reset();
    }

    private void OnExtensionResponse(BaseEvent evt)
    {
        Debug.Log("extension");

        ISFSObject objIn = (SFSObject)evt.Params["params"];
        string result = objIn.GetUtfString("Result");

        if (result == "Successful")
            Debug.Log("Successful");
        else
            Debug.Log("error");

    }//end extension

    private void reset()
    {
        // Remove SFS2X listeners
        sfs.RemoveAllEventListeners();
        sfs = null;
    }

}


