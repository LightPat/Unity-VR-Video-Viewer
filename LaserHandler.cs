using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;
using Valve.VR;

//This script handles:
//Prefab setup
//	Loading Interactables
//	Saving Interactables
//	Deleting Interactables
//Button Invoking
//	When laser pointer hits a button, invoke the button's onclick listener
//	Relies on SteamVR_LaserPointer for laser pointers on VR controllers
public class LaserHandler : MonoBehaviour
{

    public SteamVR_LaserPointer laserPointerLeft;
    public SteamVR_LaserPointer laserPointerRight;
    public GameObject VideoChangeInteractable;
    public GameObject DropdownPrefab;
    //Changes how far away interactables spawn in in the prefab setup scene
    public int spawnDistance;
    //videoIndex of the video we are displaying in videoNames
    public int videoIndex = 0;

    private SteamVR_Action_Boolean controllerInput = SteamVR_Input.GetBooleanAction("Teleport");
    private List<Interactable> interactableList = new List<Interactable>();
    private GameObject skyboxVideo;
    private List<string> videoNames = new List<string>();
    private bool mouseClickedDropDown = false;
    private bool saveOnNextFrame = false;
    private GameObject dropDownMenu;

    void Awake()
    {
        //Call this script's pointer click method after SteamVR_LaserPointer's pointer click
        laserPointerLeft.PointerClick += PointerClick;
        laserPointerRight.PointerClick += PointerClick;

        //If we're in the gamespace, load the files at our directory, and then append all those names to a list
        if (SceneManager.GetActiveScene().name == "GameSpace")
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
            //Find the skybox player in the scene, then play the first video in our list
            //Then load the corresponding video's json file which contains the locations of the prefabs
            skyboxVideo = GameObject.Find("Skybox Player");
            skyboxVideo.GetComponent<VideoPlayer>().url = Path.Combine(File_Explorer.path, videoNames[videoIndex]);
            interactableList = LoadJson(videoNames[videoIndex].Split('.')[0] + ".json");
        }
        else if (SceneManager.GetActiveScene().name == "Prefab Setup")
        {

            //If we're in prefab setup scene
            //Load the files at our directory
            //Then append all those names to a list
            //If a json file does not exist for a video, create an empty json file

            var folder = new DirectoryInfo(File_Explorer.path);
            var fileInfo = folder.GetFiles();

            foreach (var file in fileInfo)
            {
                if (file.Extension == ".mp4")
                {
                    videoNames.Add(file.Name);
                    string jsonName = file.Name.Split('.')[0] + ".json";
                    string jsonPath = Path.Combine(File_Explorer.path, jsonName);
                    if (!File.Exists(jsonPath))
                    {
                        var f = File.Create(jsonPath);
                        f.Close();
                    }
                }
            }

            //Find the skybox player in the scene, then play the first video in our list
            //Then load the corresponding video's json file which contains the locations of the prefabs
            skyboxVideo = GameObject.Find("Skybox Player");
            skyboxVideo.GetComponent<VideoPlayer>().url = Path.Combine(File_Explorer.path, videoNames[videoIndex]);
            interactableList = LoadJson(videoNames[videoIndex].Split('.')[0] + ".json");
        }
        else if (SceneManager.GetActiveScene().name == "Menu1")
        {
            if (Directory.Exists(File_Explorer.path))
            {
                GameObject.Find("FileExplorer").GetComponent<File_Explorer>().displayToCanvas();
            }
        }
    }

    void Update()
    {
        //If we're in prefab setup, check for controller input
        if (SceneManager.GetActiveScene().name == "Prefab Setup")
        {
            //Using the left hand controller
            if (laserPointerLeft.interactWithUI.GetStateUp(laserPointerLeft.pose.inputSource))
            {
                if (laserPointerLeft.ray.origin != new Vector3(0, 0, 0))
                {
                    RaycastHit hit;
                    bool bhit = Physics.Raycast(laserPointerLeft.ray, out hit);

                    if (!bhit)
                    {
                        //If the controller isn't hitting anything, spawn a prefab
                        Vector3 point = laserPointerLeft.ray.GetPoint(spawnDistance);
                        instantiateSetupPrefab(point);
                    }
                    else if (hit.transform.gameObject.tag == "VideoChange")
                    {
                        //If the controller is hitting a interactable, delete that interactable
                        interactableList.Remove(new Interactable(hit.transform.position, hit.transform.gameObject.GetComponent<VideoChange>().newClip));
                        Destroy(hit.transform.gameObject);
                        SaveLocations(interactableList);
                    }
                }
            }

            //Using the right hand controller
            if (laserPointerRight.interactWithUI.GetStateUp(laserPointerRight.pose.inputSource))
            {
                if (laserPointerRight.ray.origin != new Vector3(0, 0, 0))
                {
                    RaycastHit hit;
                    bool bhit = Physics.Raycast(laserPointerRight.ray, out hit);
                    if (!bhit)
                    {
                        //If the controller isn't hitting anything, spawn a prefab
                        Vector3 point = laserPointerRight.ray.GetPoint(spawnDistance);
                        instantiateSetupPrefab(point);
                    }
                    else if (hit.transform.gameObject.tag == "VideoChange")
                    {
                        //If the controller is hitting a interactable, delete that interactable
                        interactableList.Remove(new Interactable(hit.transform.position, hit.transform.gameObject.GetComponent<VideoChange>().newClip));
                        Destroy(hit.transform.gameObject);
                        SaveLocations(interactableList);
                    }
                }
            }

            if (saveOnNextFrame)
            {
                GameObject[] arr = GameObject.FindGameObjectsWithTag("VideoChange");

                interactableList = new List<Interactable>();

                foreach (GameObject g in arr)
                {
                    interactableList.Add(new Interactable(g.transform.position, g.GetComponent<VideoChange>().newClip));
                }

                SaveLocations(interactableList);
                Debug.Log(arr.Length + " " + interactableList.Count);
                saveOnNextFrame = false;
            }

            if (mouseClickedDropDown)
            {
                Transform list = dropDownMenu.transform.Find("Dropdown List");
                Transform viewport = list.transform.Find("Viewport");
                Transform content = viewport.transform.Find("Content");

                foreach (Transform child in content.transform)
                {
                    GameObject g = child.gameObject;
                    // Add colliders to UI elements so that laser pointer can hit them
                    if (g.activeInHierarchy)
                    {
                        BoxCollider col = g.AddComponent<BoxCollider>();
                        col.center = new Vector3(-0.4f, 0.3f, -0.05f);
                        col.size = new Vector3(300, 22, 6);
                    }
                }

                mouseClickedDropDown = !mouseClickedDropDown;
            }

            // If you left click your mouse
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, spawnDistance));
                Ray ray = new Ray(Camera.main.transform.position, point);
                RaycastHit hit;
                bool bHit = Physics.Raycast(ray, out hit);

                if (!bHit)
                {
                    instantiateSetupPrefab(point);
                    saveOnNextFrame = true;
                }
                else if (hit.transform.gameObject.tag == "VideoChange")
                {
                    Destroy(hit.transform.gameObject);
                    //Destroy only destroys the object AFTER this frame, which is why saveOnNextFrame is called above this if statement
                    saveOnNextFrame = true;
                }
                else if (hit.transform.tag == "FileDisplay")
                {
                    mouseClickedDropDown = !mouseClickedDropDown;
                    dropDownMenu = hit.collider.gameObject;
                }
                else if (hit.transform.gameObject.GetComponent<Toggle>() != null)
                {
                    GameObject canvas = hit.transform.parent.parent.parent.parent.parent.gameObject;
                    string newClip = hit.transform.Find("Item Label").GetComponent<Text>().text;
                    Vector3 savePoint = canvas.transform.parent.position;
                    GameObject interactable = canvas.transform.parent.gameObject;
                    interactable.GetComponent<VideoChange>().newClip = newClip;

                    Destroy(canvas);
                    saveOnNextFrame = true;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "GameSpace")
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, spawnDistance));
            Ray ray = new Ray(Camera.main.transform.position, point);
            RaycastHit hit;
            bool bHit = Physics.Raycast(ray, out hit);

            if (hit.transform.gameObject.tag == "VideoChange")
            {
                hit.transform.gameObject.GetComponent<VideoChange>().Activate();
            }
        }

        //This is checked regardless of what scene we're in
        if ((controllerInput.GetState(laserPointerRight.pose.inputSource) | controllerInput.GetState(laserPointerLeft.pose.inputSource) | Input.GetKey(KeyCode.Escape)) & SceneManager.GetActiveScene().name != "Menu1")
        {
            SceneManager.LoadScene("Menu1");
        }
    }

    //Built in method append to SteamVR_LaserPointer in StreamVR extras.
    public void PointerClick(object sender, PointerEventArgs e)
    {
        GameObject clickedObject = e.target.gameObject;

        //If the gameObject hit is a video change interactable, activate the video change
        if (clickedObject.tag == "VideoChange")
        {
            if (SceneManager.GetActiveScene().name == "GameSpace")
            {
                clickedObject.GetComponent<VideoChange>().Activate();
            }
        }

        //UI is ID'd 5 in the layers
        if (clickedObject.layer == 5)
        {
            //Dropdown menu handling
            if (clickedObject.GetComponent<Dropdown>() != null)
            {
                clickedObject.GetComponent<Dropdown>().Hide();
                clickedObject.GetComponent<Dropdown>().Show();

                Transform list = clickedObject.transform.Find("Dropdown List");
                Transform viewport = list.transform.Find("Viewport");
                Transform content = viewport.transform.Find("Content");

                foreach (Transform child in content.transform)
                {
                    GameObject g = child.gameObject;
                    // Add colliders to UI elements so that laser pointer can hit them
                    if (g.activeInHierarchy)
                    {
                        BoxCollider col = g.AddComponent<BoxCollider>();
                        col.center = new Vector3(-0.4f, 0.3f, -0.05f);
                        col.size = new Vector3(300, 22, 6);
                    }
                }
            }

            //Dropdown menu handling after it is shown (handles selection of elements)
            if (clickedObject.GetComponent<Toggle>() != null)
            {
                if (clickedObject.GetComponent<Toggle>().isOn)
                {
                    clickedObject.GetComponent<Toggle>().isOn = false;
                }
                else
                {
                    clickedObject.GetComponent<Toggle>().isOn = true;
                    if (SceneManager.GetActiveScene().name == "Menu1")
                    {
                        File_Explorer.firstVideoName = clickedObject.transform.Find("Item Label").gameObject.GetComponent<Text>().text;
                    }
                    else if (SceneManager.GetActiveScene().name == "Prefab Setup")
                    {
                        GameObject canvas = clickedObject.transform.parent.parent.parent.parent.gameObject;
                        string newClip = clickedObject.transform.Find("Item Label").GetComponent<Text>().text;
                        Vector3 point = canvas.transform.parent.position;
                        interactableList.Add(new Interactable(point, newClip));
                        Destroy(canvas);
                        SaveLocations(interactableList);
                    }
                }
            }

            //Button handling
            if (clickedObject.GetComponent<Button>() != null)
            {
                clickedObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    //Object structure for json serialization later
    public struct Interactable
    {
        public Vector3 point;
        public string newClip;

        public Interactable(Vector3 point, string newClip)
        {
            this.point = point;
            this.newClip = newClip;
        }
    }

    //Instantiates a prefab at a given point
    public void instantiateSetupPrefab(Vector3 point)
    {
        GameObject instantiated = GameObject.Instantiate(VideoChangeInteractable, point, Camera.main.transform.rotation);

        GameObject dropdownMenu = GameObject.Instantiate(DropdownPrefab, new Vector3(point.x, point.y + 1, point.z), Camera.main.transform.rotation, instantiated.transform);
        List<string> options = new List<string>(videoNames);
        options.Insert(0, "");
        dropdownMenu.transform.GetChild(0).GetComponent<Dropdown>().AddOptions(options);
    }

    //Instantiates a prefab at a given point
    public void instantiatePrefab(Vector3 point, string vidName)
    {
        GameObject instantiated = GameObject.Instantiate(VideoChangeInteractable, point, Camera.main.transform.rotation);

        instantiated.GetComponent<VideoChange>().newClip = vidName;
    }

    //Used in the prefab setup scene
    public void nextSetupVideo()
    {
        //If there is a next video, change skybox to next video and load corresponding prefabs
        if (videoIndex < videoNames.Count - 1)
        {
            videoIndex += 1;
            skyboxVideo.GetComponent<VideoPlayer>().url = Path.Combine(File_Explorer.path, videoNames[videoIndex]);
            var prefabs = GameObject.FindGameObjectsWithTag("VideoChange");
            foreach (var prefab in prefabs)
            {
                Destroy(prefab);
            }
            interactableList = LoadJson(videoNames[videoIndex].Split('.')[0] + ".json");
        }
    }

    //Used in the prefab setup scene
    public void prevSetupVideo()
    {
        //If there is a previous video, change skybox to previous video and load corresponding prefabs
        if (videoIndex > 0)
        {
            videoIndex -= 1;
            skyboxVideo.GetComponent<VideoPlayer>().url = Path.Combine(File_Explorer.path, videoNames[videoIndex]);
            var prefabs = GameObject.FindGameObjectsWithTag("VideoChange");
            foreach (var prefab in prefabs)
            {
                Destroy(prefab);
            }
            interactableList = LoadJson(videoNames[videoIndex].Split('.')[0] + ".json");
        }
    }

    //Save json file of locations list
    public void SaveLocations(List<Interactable> structList)
    {
        string json = "";

        for (int i = 0; i < structList.Count; i++)
        {
            json += JsonUtility.ToJson(structList[i]);
        }

        //Filename is the path to the json file, which is just the video name concatenated with .json
        string filename = Path.Combine(File_Explorer.path, videoNames[videoIndex].Split('.')[0] + ".json");
        File.WriteAllText(filename, json);

        Debug.Log(structList.Count + "...Interactable data saved at " + filename);
    }

    //Parses json of vector3s at a given filename
    public List<Interactable> LoadJson(string jsonName)
    {
        string path = File_Explorer.path;

        string jsonPath = Path.Combine(path, jsonName);
        Debug.Log("Reading json from: " + jsonPath);

        // Reads in json string from file
        StreamReader r = new StreamReader(jsonPath);
        string jsonString = r.ReadToEnd();

        //This is the parsed json string that we will turn into an Interactable struct
        string jsonObject = "";
        //Tracks if we've parsed a json object
        string tracker = "";
        List<Interactable> jsonList = new List<Interactable>();

        //Parses objects from json string
        foreach (char c in jsonString)
        {
            switch (c)
            {
                case '{':
                    tracker += c;
                    break;
                case '}':
                    tracker += c;
                    break;
            }
            if (tracker == "{{}}")
            {
                tracker = "";
                jsonObject += c;
                jsonList.Add(JsonUtility.FromJson<Interactable>(jsonObject));
                jsonObject = "";
            }
            else
            {
                jsonObject += c;
            }
        }

        //Instantiate a new prefab at the points in the list, with each corresponding newClip string
        foreach (Interactable x in jsonList)
        {
            instantiatePrefab(x.point, x.newClip);
        }

        return jsonList;
    }
}