using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoChange : MonoBehaviour
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
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			Physics.Raycast(ray, out hit);
			
			if (hit.collider.gameObject.CompareTag("VideoChange"))
			{
				Debug.Log("COLLISION detected...");
				Activate();
			}
		}
	}

	public void Activate() {
		Debug.Log("Changing Video");
		videoPlayer.clip = newClip;
	}
}
