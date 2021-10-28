using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class LaserHandler : MonoBehaviour
{
	//Uses the built in laser pointer functionality that comes with steamvr package
    //to create a collision detection system just like a mouse click
    public SteamVR_LaserPointer laserPointerLeft;
    public SteamVR_LaserPointer laserPointerRight;
    public GameObject eventSystem;

    void Awake()
    {
        laserPointerLeft.PointerClick += PointerClick;
		laserPointerRight.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
		GameObject clickedObject = e.target.gameObject;

	    Debug.Log(clickedObject.name);

	    if(clickedObject.tag == "VideoChange") {
		    clickedObject.GetComponent<VideoChange>().activate();
	    }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
    }
}