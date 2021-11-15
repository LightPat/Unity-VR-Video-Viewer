using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class File_Explorer : MonoBehaviour
{
   
    public string path;
    //Change this reference later, don't want to have to assign canvas everytime
    public GameObject canvas;
    [SerializeField]
    public GameObject prefab;


    void Start() {
	//set the path in the inspector

	//get the directory data (all files in directory)
	var folder = new DirectoryInfo(path);
        var fileInfo = folder.GetFiles();

	List<string> fileNames = new List<string>();
	List<float> fileSizes = new List<float>();

	//get the file attributes (name, length, etc)
	foreach (var file in fileInfo) {
		if (file.Extension == ".mp4") {
			fileNames.Add(file.Name);
			//convert to megabytes
			fileSizes.Add((float)file.Length/1048576);
		}
	}

	var yOffset = 25;

	for(int i = 0; i < fileNames.Count; i++) {

		var xOffset = -50;

		//displays text prefabs as file NAMES
		var prefabInstance = Instantiate(prefab);
		prefabInstance.transform.SetParent(canvas.transform, false);

		var textComponent = prefabInstance.GetComponent<Text>();
		textComponent.text = fileNames[i];

		// changes transform by the offset on the x axis
		prefabInstance.transform.Translate(new Vector3(xOffset, yOffset, 0));

		xOffset += 75;

		//displays text prefabs as file SIZES
		prefabInstance = Instantiate(prefab);
		prefabInstance.transform.SetParent(canvas.transform, false);
	
		textComponent = prefabInstance.GetComponent<Text>();
		textComponent.text = fileSizes[i].ToString() + " MB";
		
		prefabInstance.transform.Translate(new Vector3(xOffset, yOffset, 0));

		yOffset -= 10;
	}

	//Prints name and size to console
	for (int i = 0; i < fileNames.Count; i++) {
		Debug.Log("Name: " + fileNames[i] + "    Size: " + fileSizes[i]);
	}
    }    
}



