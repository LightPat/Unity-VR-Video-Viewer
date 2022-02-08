using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInteractable : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    public GameObject VideoChangeInteractable;

    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
	//Not being used right now
        if (Input.GetMouseButtonUp(0)) //mouse left click
        {
            Debug.Log("Clicked");

            //mousePos and point are used for instantiating new interactables at a particular location
            Vector3 mousePos = Input.mousePosition;
            Vector3 point = new Vector3();

            //converts mouse position on the screen to coordinates in the game space
            point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane+10));

            GameObject.Instantiate(VideoChangeInteractable, point, Quaternion.identity);
        }
   }


   public void instantiateAtPoint(Vector3 point) {
	GameObject.Instantiate(VideoChangeInteractable, point, Quaternion.identity);
   }
}
