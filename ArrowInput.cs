using UnityEngine;

/*
 * ArrowInput handles listeners for specific letter key inputs for 2D-functionality.
 *
 * Arrow Keys or A, W, S, D keys cause camera rotation
 * R button resets camera position to default rotation
 * Escape button takes user back to the main menu (this functionality is in LaserHandler.cs)
 * Q button quits the application
 *
 * Last Updated: December 9, 2021
 *
 * Author: Westin Fishel, CS-470/CS-475, Software Engineering Project, "Beta Squad"
 */
public class ArrowInput : MonoBehaviour
{
    //xCamRot stores VERTICAL angle (x-axis); yCamRot stores HORIZONTAL angle (y-axis)
    public float xCamRot, yCamRot;

    //Booleans track the acceptable conditions of looking up and down!
    public bool isUpAcceptable, isDownAcceptable;

    // Update is called once per frame
    private void Update()
    {
        xCamRot = transform.eulerAngles.x;
        yCamRot = transform.eulerAngles.y;
        
        //Values overlap because identical limits compensates glitch where rotation creeps beyond limit.
        isUpAcceptable = xCamRot <= 90 || xCamRot >= 280 && xCamRot <= 365;
        isDownAcceptable = xCamRot <= 80 || xCamRot >= 270 && xCamRot <= 365;

        //Loops handle combination of 1-2 arrow key(s)/A, W, S, D inputs.
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            transform.rotation = isUpAcceptable
                ? Quaternion.Euler(xCamRot - 2, yCamRot - 2, 0)
                : Quaternion.Euler(xCamRot, yCamRot - 2, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            transform.rotation = isDownAcceptable
                ? Quaternion.Euler(xCamRot + 2, yCamRot - 2, 0)
                : Quaternion.Euler(xCamRot, yCamRot - 2, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = isUpAcceptable
                ? Quaternion.Euler(xCamRot - 2, yCamRot + 2, 0)
                : Quaternion.Euler(xCamRot, yCamRot + 2, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = isDownAcceptable
                ? Quaternion.Euler(xCamRot + 2, yCamRot + 2, 0)
                : Quaternion.Euler(xCamRot, yCamRot + 2, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.rotation = isUpAcceptable
                ? Quaternion.Euler(xCamRot - 2, yCamRot, 0)
                : Quaternion.Euler(xCamRot, yCamRot, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(xCamRot, yCamRot - 2, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.rotation = isDownAcceptable
                ? Quaternion.Euler(xCamRot + 2, yCamRot, 0)
                : Quaternion.Euler(xCamRot, yCamRot, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(xCamRot, yCamRot + 2, 0);
        }

        //R stands for reset - resets camera's position
        else if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        //Q stands for quit - closes out the entire application
        else if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
            Debug.Log("Quitting the application");
        }
    }
}