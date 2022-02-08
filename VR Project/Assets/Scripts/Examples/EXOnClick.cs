using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXOnClick : MonoBehaviour
{

	void Start()
	{
		//references the current gameObject script is attached to, finds the button component, then adds a listener for a click
		gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
	}
}
