using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClickSceneChange : MonoBehaviour
{
	//Used on a button to change a scene
	void Start()
	{
		//references the current gameObject script is attached to, finds the button component, then adds a listener for a click
		gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Button clicked! Changing Scene...");
		SceneManager.LoadScene("1GameSpace");
	}
}
