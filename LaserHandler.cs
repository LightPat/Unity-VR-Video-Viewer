using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class LaserHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointerLeft;
    public SteamVR_LaserPointer laserPointerRight;

    private GameObject interactableMaker;

    void Awake()
    {
        laserPointerLeft.PointerClick += PointerClick;
	laserPointerRight.PointerClick += PointerClick;

	GameObject[] array = GameObject.FindGameObjectsWithTag("InteractableCreator");
	foreach (GameObject g in array) {
		interactableMaker = g;
	}
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
	GameObject clickedObject = e.target.gameObject;

	Debug.Log("Collided with: " + clickedObject.name);
	
	if(clickedObject.tag == "VideoChange") {
		clickedObject.GetComponent<VideoChange>().Activate();
	}
	
	//UI is ID'd 5 in the layers
	if(clickedObject.layer == 5) {
		Debug.Log("This is a UI element. WE ONLY SUPPORT BUTTONS RIGHT NOW");
		clickedObject.GetComponent<Button>().onClick.Invoke();
	}

	// and the scene is coming from the setup wizard
	if(interactableMaker != null) {
		Debug.Log(e);
	}
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
    }
}