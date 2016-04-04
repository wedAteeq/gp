using UnityEngine;
using UnityEditor;
using System.Collections;
using Sfs2X;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;



public class Room : MonoBehaviour
{


    public Animator anim; //to attach the avatar
    Collider FramePic; //referrnse to frame of picture
    string filePath = ""; //the path of the picture 
    Texture2D texture; //the picture itself
    int flag; // i will use it for arrange
    public Material Clear;     //matrial that in delete 
    public GameObject add;     //for add button 
    public GameObject delete;  //for delete button
    public GameObject arrange; //for arrange button 
    private SmartFox sfs;
    private string userName, accountType;
    private int RoomId;

    void Start()
    {
        anim = GetComponent<Animator>(); //define the avatar
        add.SetActive(false);
        delete.SetActive(false);
        arrange.SetActive(false);

    }



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
    // Use this for initialization

    void OnTriggerEnter(Collider other) //if the user touch anu collider

    {
        if (other.gameObject.CompareTag("Frame"))  //  to know which fram will be affect  

        {

            FramePic = other; //assign the touced object to FramePic

        }

        if (other.gameObject.CompareTag("ActiveGround"))
        {
            add.SetActive(true);
            delete.SetActive(true);
            arrange.SetActive(true);


        }


        if (other.gameObject.CompareTag("DisactiveGround"))
        {

            add.SetActive(false);
            delete.SetActive(false);
            arrange.SetActive(false);
        }
        

    }

    public void AddContents()

    {
#if UNITY_EDITOR
        filePath = EditorUtility.OpenFilePanel("Overwrite with png"
                                            , Application.streamingAssetsPath
                                            , "png");  //to open panel so the user will choose picture from his pc and save file path
#endif
        if (filePath.Length != 0)  //if the user choose picture that means filePath not empty
        {
            WWW www = new WWW("file://" + filePath);
            texture = new Texture2D(64, 64);
            www.LoadImageIntoTexture(texture);  //load image as Texture
            FramePic.GetComponent<Renderer>().material.mainTexture = texture; //assign Texture to FramePic (touced object)

        }

    }


    public void DeleteContents()

    {


        if (EditorUtility.DisplayDialog("Warning Message", "Are you sure you want to delete this content?", "OK", "Cancel")) 
        {
            FramePic.GetComponent<Renderer>().material.mainTexture = null; //delete the texture 
            FramePic.GetComponent<Renderer>().material = Clear; //assign this standerd matrial 
        }


    }

    public void ArrangeContents()
    {



        if (EditorUtility.DisplayDialog("Warning Message", "Are you sure you want to rearrange this fram?", "OK", "Cancel"))
        {


            FramePic.transform.parent = anim.transform; //make the frame child of the avatar 
            FramePic.transform.position = anim.transform.forward + anim.transform.up + anim.transform.up + anim.transform.position; //to make the object in frount of the avatar
            //FramePic.transform.rotation = new Quaternion (0f,180f,0f,0f);


        }
        EditorUtility.DisplayDialog("Warning Message", "Please press Z if you find the appropriate postion for this frame ", "OK");
    }


    void Update()
    {
        // As Unity is not thread safe, we process the queued up callbacks on every frame
        if (sfs != null)
            sfs.ProcessEvents();

        //I will use when the user carry the frame
        if (Input.GetKey(KeyCode.Z))
            FramePic.transform.parent = null;
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

