using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDebug : MonoBehaviour
{
    void Update()
    {
	if (Input.GetButtonDown("Fire1")) {
		Vector3 mousePos = Input.mousePosition;
		Debug.DrawRay(mousePos, mousePos, Color.green);
	}
    }
}
