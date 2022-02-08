using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXGameObject : MonoBehaviour
{
    public GameObject otherObject;

    void Start()
    {
        // Prints out the name of the gameObject the script is attached to
        Debug.Log(gameObject.name);

        if (otherObject != null)
        {
            // Prints out the name of a gameObject that you set in the inspector
            Debug.Log(otherObject.name);
        }
    }
}
