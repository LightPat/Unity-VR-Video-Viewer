using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseClick : MonoBehaviour
{
    public GameObject VideoChangeInteractable;
    public int spawnDistance;

    private Camera cam;
    private LaserHandler laserHandler;
    private List<LaserHandler.Interactable> interactableList = new List<LaserHandler.Interactable>();
    private List<string> videoNames = new List<string>();

    void Start()
    {
        cam = Camera.main;
        laserHandler = cam.transform.parent.GetComponent<LaserHandler>();

        if (SceneManager.GetActiveScene().name == "Prefab Setup")
        {
            var folder = new DirectoryInfo(File_Explorer.path);
            var fileInfo = folder.GetFiles();

            foreach (var file in fileInfo)
            {
                if (file.Extension == ".mp4")
                {
                    videoNames.Add(file.Name);
                }
            }
        }
    }

    void Update()
    {
        // Checks for mouse input to be on the button
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Prefab Setup")
        {
            Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, spawnDistance));

            RaycastHit hit;
            bool bHit = Physics.Raycast(cam.transform.position, point, out hit);

            if (!bHit)
            {
                GameObject g = GameObject.Instantiate(VideoChangeInteractable, point, Quaternion.identity);
                interactableList.Add(new LaserHandler.Interactable(point, videoNames[laserHandler.videoIndex + 1]));
                laserHandler.SaveLocations(interactableList);
            }
        }
    }
}
