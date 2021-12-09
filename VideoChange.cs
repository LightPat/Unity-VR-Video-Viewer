using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoChange : MonoBehaviour
{
    public string newClip;
    public GameObject VideoChangeInteractable;

    private LaserHandler laserHandler;

    void Awake()
    {
        //Finds the laser handler script in the scene so that we can use the methods contained within
        laserHandler = GameObject.Find("[CameraRig]").GetComponent<LaserHandler>();
    }

    public void Activate()
    {
        //Play the next video attached to this script in the skybox
        GameObject skyboxVideo = GameObject.Find("Skybox Player");
        skyboxVideo.GetComponent<VideoPlayer>().url = Path.Combine(File_Explorer.path, newClip);

        //Finds all other interactables in the scene
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("VideoChange");

        //Destroys all other interactables if there are any
        foreach (GameObject g in interactables)
        {
            if (g != gameObject)
            {
                Destroy(g);
            }
        }

        //Load json interactable data and instantiate prefabs in scene for the next video
        string jsonName = newClip.Split('.')[0] + ".json";
        laserHandler.LoadJson(jsonName);

        //Destroys this interactable, creating a clean slate for the next video and interactables to play
        Destroy(gameObject);
    }
}
