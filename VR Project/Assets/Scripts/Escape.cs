using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) & SceneManager.GetActiveScene().name != "Menu1")
        {
            SceneManager.LoadScene("Menu1");
        }
    }
}
