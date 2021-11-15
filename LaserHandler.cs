using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class LaserHandler : MonoBehaviour
{

    public SteamVR_LaserPointer laserPointerLeft;
    public SteamVR_LaserPointer laserPointerRight;
    //Changes how far away interactables spawn in in the prefab setup scene
    public int spawnDistance;

    private GameObject interactableMaker;
    [SerializeField]
    private List<Vector3> locations = new List<Vector3>();

    void Awake()
    {
        laserPointerLeft.PointerClick += PointerClick;
	laserPointerRight.PointerClick += PointerClick;	

	GameObject[] array = GameObject.FindGameObjectsWithTag("InteractableCreator");
	foreach (GameObject g in array) {
		interactableMaker = g;
	}
    }

    void Update() {
	if (SceneManager.GetActiveScene().name == "Prefab Setup") {
		if (laserPointerLeft.interactWithUI.GetStateUp(laserPointerLeft.pose.inputSource)) {
			if (laserPointerLeft.ray.origin != new Vector3(0,0,0)) {
				if (interactableMaker != null) {
					Vector3 point = laserPointerLeft.ray.GetPoint(spawnDistance);
					interactableMaker.GetComponent<CreateInteractable>().instantiateAtPoint(point);
					locations.Add(point);
				}
			}
		}

		if (laserPointerRight.interactWithUI.GetStateUp(laserPointerRight.pose.inputSource)) {
			if (laserPointerRight.ray.origin != new Vector3(0,0,0)) {
				if (interactableMaker != null) {
					Vector3 point = laserPointerRight.ray.GetPoint(spawnDistance);
					interactableMaker.GetComponent<CreateInteractable>().instantiateAtPoint(point);
					locations.Add(point);
				}
			}
		}

		if (Input.GetKey(KeyCode.LeftShift) & Input.GetKeyDown("s")) {
			SaveLocations();
		}

	}
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
	GameObject clickedObject = e.target.gameObject;

	//Debug.Log("Collided with: " + clickedObject.name);
	
	if(clickedObject.tag == "VideoChange") {
		if(SceneManager.GetActiveScene().name == "GameSpace") {
			clickedObject.GetComponent<VideoChange>().Activate();
		}
	}
	
	//UI is ID'd 5 in the layers
	if(clickedObject.layer == 5) {
		Debug.Log("This is a UI element. WE ONLY SUPPORT BUTTONS RIGHT NOW");
		clickedObject.GetComponent<Button>().onClick.Invoke();
	}
    }

    void SaveLocations() {
	string json = "";

	for(int i = 0; i < locations.Count; i++) {
		json += JsonUtility.ToJson(locations[i]);
	}

	string filename = Path.Combine(@"H:\Desktop", "locations.json");

	File.WriteAllText(filename, json);

	Debug.Log("Location data saved at " + filename);
    }
}