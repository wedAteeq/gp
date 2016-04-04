using UnityEngine;
using UnityEditor;
using Sfs2X;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;

public class Room 
{

   
    public Collider FramePic; //referrnse to frame of picture 
    public string filePath = ""; //the path of the picture 
    public Texture2D texture; //the picture itself

    private SmartFox sfs;
    private string userName, accountType;
    private int RoomId;

    public int getRoomId() { return RoomId; }
    //Manage Room (Add/Delete/Like/Arrang contents) 
    public Room()
    {
        RoomId = 0;
        userName = "";
        accountType = "";
    }
    public Room(int RoomID,string user, string accType)
    {
        RoomId= RoomID;
        userName = user;
        accountType = accType;
    }

    void OnTriggerEnter(Collider other) // to know which fram will be affect 

    {
        if (other.gameObject.CompareTag("Frame"))  // we will know from tag 

        {
            other.gameObject.SetActive(false);
            FramePic = other;
        }
    }

    public void AddContents()

    {
#if UNITY_EDITOR
        filePath = EditorUtility.OpenFilePanel("Overwrite with png"
                                            , Application.streamingAssetsPath
                                            , "png");
#endif
        if (filePath.Length != 0)
        {
            WWW www = new WWW("file://" + filePath);
            texture = new Texture2D(64, 64);
            www.LoadImageIntoTexture(texture);
            FramePic.GetComponent<Renderer>().material.mainTexture = texture;

        }

    }


    public void DeleteContents()

    {
        FramePic.GetComponent<Renderer>().material.mainTexture = null;
    }


    void Update()
    {
        // As Unity is not thread safe, we process the queued up callbacks on every frame
        if (sfs != null)
            sfs.ProcessEvents();
    }


    public void CreateRoom(SmartFox sfs2x, int Room_ID,string user, string account)
    {
        sfs = sfs2x;

        Debug.Log("in method ");
        userName = user;
        accountType = account;
        ISFSObject objOut = new SFSObject();
        objOut.PutUtfString("Room_ID", Room_ID+"");
        objOut.PutUtfString("username", userName);
        objOut.PutUtfString("accountType", accountType);
        sfs.Send(new ExtensionRequest("CreateRoom", objOut));

    }

    public void DeleteRoom(SmartFox sfs2x, string user)
    {
        sfs = sfs2x;

        Debug.Log("in method ");
        userName = user;
        ISFSObject objOut = new SFSObject();
        objOut.PutUtfString("username", userName);
         sfs.Send(new ExtensionRequest("DeleteRoom", objOut));
    }

    public void getAllRooms(SmartFox sfs2x)
    {
        sfs = sfs2x;
        Debug.Log("in method ");
        ISFSObject objOut = new SFSObject();
        sfs.Send(new ExtensionRequest("GetRooms", objOut));
    }
}

