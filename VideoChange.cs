using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoChange : MonoBehaviour
{
	//Used on a gameobject to change the video clip playing in the skybox when it is clicked
	VideoPlayer videoPlayer;
	public VideoClip newClip;

	void Start()
    {
		videoPlayer = gameObject.GetComponent<VideoPlayer>();
    }

	void Update()
	{
		// Check for mouse input
		if (Input.GetMouseButton(0))
		{
			Debug.Log("Working");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			Physics.Raycast(ray, out hit);
			
			if (hit.collider.gameObject.tag == "VideoChange")
			{
				Debug.Log("COLLISION detected...");
				activate();
			}
		}
	}

	public void activate() {
		Debug.Log("Changing Video");
		videoPlayer.clip = newClip;
	}
}
