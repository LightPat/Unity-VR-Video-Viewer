using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
This script simply changes the scene on a button click!
*/
public class OnClickSceneChange : MonoBehaviour
{

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
