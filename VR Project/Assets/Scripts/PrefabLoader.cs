using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    private Camera cam;
    public GameObject VideoChangeInteractable;
    
    private string path;

    void Start()
    {
        cam = Camera.main;

	path = File_Explorer.path;
	LoadJson();
    }

   public void instantiateAtPoint(Vector3 point) {
	GameObject.Instantiate(VideoChangeInteractable, point, Quaternion.identity);
   }

   public void LoadJson() {
	string jsonPath = Path.Combine(path, "locations.json");
	Debug.Log("Reading json from: " + jsonPath);

	// Reads in json string from file
	StreamReader r = new StreamReader(jsonPath);
	string jsonString = r.ReadToEnd();

	string vector3String = "";
	List<Vector3> positions = new List<Vector3>();

	// Parses vector3 objects from json string
	foreach (char c in jsonString) {
		switch (c)
		{
			case '{':
				vector3String = "";
				vector3String += c;
				break;
			case '}':
				vector3String += c;
				positions.Add(JsonUtility.FromJson<Vector3>(vector3String));
				break;
			default:
				vector3String += c;
				break;
		}
	}


	// Instantiate a new prefab at each point specified in the json file
	foreach (Vector3 p in positions) {
		instantiateAtPoint(p);
	}
   }
}
