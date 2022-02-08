using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public void mainMenu() {
		SceneManager.LoadScene("Menu1");
	}

	public void gameSpace() {
		SceneManager.LoadScene("GameSpace");
	}

	public void prefabSetup() {
		SceneManager.LoadScene("Prefab Setup");
	}
}
