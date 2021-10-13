using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickQuit : MonoBehaviour
{
    //Quits the application when a button is pressed
    void Start()
    {
        //references the current gameObject script is attached to, finds the button component, then adds a listener for a click
        gameObject.GetComponent<Button>().onClick.AddListener(quitGame);
    }

    public void quitGame()
    {
        Debug.Log("Quitting the application");
        Application.Quit();
    }
}
