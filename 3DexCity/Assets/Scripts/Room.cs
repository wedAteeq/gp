using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text;

public class Room : MonoBehaviour
{

   
    public Collider FramePic; //referrnse to frame of picture 
    public string filePath = ""; //the path of the picture 
    public Texture2D texture; //the picture itself

    //Manage Room (Add/Delete/Like/Arrang contents) 


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



}
