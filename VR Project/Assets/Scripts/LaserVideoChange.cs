using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LaserVideoChange : MonoBehaviour
{

	VideoPlayer videoPlayer;
	public VideoClip newClip;

	void Start()
    	{
		videoPlayer = gameObject.GetComponent<VideoPlayer>();
    	}

	void Update()
	{
		// Check for mouse input
/*		
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			Physics.Raycast(ray, out hit);

			if (hit.collider.gameObject.tag == "VideoChange")
			{
				Debug.Log("COLLISION detected...");
				videoPlayer.clip = newClip;
			}
		}
*/
	}
}