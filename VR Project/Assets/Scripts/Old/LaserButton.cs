using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class LaserButton : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointerLeft;
    public SteamVR_LaserPointer laserPointerRight;

    void Awake()
    {
        laserPointerLeft.PointerClick += PointerClick;
	laserPointerRight.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
	Debug.Log(e);
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
    }
}