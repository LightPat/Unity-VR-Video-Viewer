using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class File_Explorer : MonoBehaviour
{
   
    //Change this reference later, don't want to have to assign canvas everytime
    public GameObject objectDisplay;
    public GameObject prefab;
	public static string path;
	public static string firstVideoName;

	private GameObject eventSystem;
	private List<string> fileNames;
	private List<float> fileSizes;

	public void browse()
    {
		eventSystem = GameObject.Find("EventSystem");
		eventSystem.SetActive(false);

		StartCoroutine(ShowLoadDialogCoroutine());
	}

	public void displayToCanvas()
    	{
		// Fixes the path if the user selects a file as the end of the path instead of just the folder
		if (!Directory.Exists(path))
        	{
			string[] folders = path.Split('\\');
			string lastFileName = folders[folders.Length-1];
			path = path.Replace("\\" + lastFileName, "");
		}

		fileSizes = new List<float>();
		fileNames = new List<string>();

		//get the directory data (all files in directory)
		var folder = new DirectoryInfo(path);
		var fileInfo = folder.GetFiles();

		//get the file attributes (name, length, etc)
		foreach (var file in fileInfo)
		{
			if (file.Extension == ".mp4")
			{
				fileNames.Add(file.Name);
				//convert to megabytes
				fileSizes.Add((float)file.Length / 1048576);
			}
		}

		GameObject countDisplay = GameObject.Find("VideoCount");
		countDisplay.GetComponent<Text>().text = "Video Count: " + fileNames.Count.ToString();

		GameObject dropdownMenu = GameObject.Find("Dropdown");
		dropdownMenu.GetComponent<Dropdown>().AddOptions(fileNames);

		firstVideoName = fileNames[0];

	}

	IEnumerator ShowLoadDialogCoroutine()
    	{
		// Only show mp4 files and folders
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Videos", ".mp4"));

		FileBrowser.SetDefaultFilter(".mp4");
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

		// If the file browser has been closed
		if (FileBrowser.Success)
		{
			path = FileBrowser.Result[0];
		}
		eventSystem.SetActive(true);
		displayToCanvas();
	}
}



