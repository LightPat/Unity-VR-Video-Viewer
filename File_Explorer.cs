using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;


//change file into a videoclip object
//google unity videoclip
public class File_Explorer : MonoBehaviour
{
   
    public string path; //folder
    VideoPlayer player; //video player component

    void Start() {
        player = gameObject.GetComponent<VideoPlayer>();

        var folder = new DirectoryInfo(path);
        var fileInfo = folder.GetFiles();
        foreach (var file in fileInfo) {
            //File name
            //Debug.Log(file.Name);
            //File size
            //Debug.Log(file.Length);

            if(file.Extension == ".mp4")
            {
                player.url = file.DirectoryName + "\\" + file.Name;
                Debug.Log("Length " + player.length);
                //Debug.Log(file.DirectoryName + "\\" + file.Name);
                //Debug.Log("Video found " + file.Extension);
            } else
            {
                //Debug.Log(file.Extension);
            }
        }
    }

    /*
    public void OpenFileExplorer ()
    {
        //path = EditorUtility.OpenFilePanel("show all videos (.mp4)", "", "mp4");
        
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + path);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            GetTexture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rawVideo.texture = myTexture;

        }
    }
    */
}

