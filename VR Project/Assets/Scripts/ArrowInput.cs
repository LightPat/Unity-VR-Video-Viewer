using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * ArrowInput handles listeners for specific letter key inputs for keyboard functionality.
 *
 * Arrow Keys or A, W, S, D keys cause camera rotation
 * R button resets camera position to default rotation
 * Escape button takes user back to the main menu (this functionality is in LaserHandler.cs)
 * Q button quits the application
 * H/L buttons make camera sensitivity higher and lower respectively
 * I inverts camera vertical rotation
 * M button takes user back to main menu
 *
 * Last Updated: December 10, 2021
 *
 * Author: Westin Fishel, CS-470/CS-475, Software Engineering Project, "Beta Squad"
 */
public class ArrowInput : MonoBehaviour
{
    //xCamRot stores VERTICAL angle (x-axis); yCamRot stores HORIZONTAL angle (y-axis)
    public float xCamRot, yCamRot;

    //Booleans track the acceptable conditions of looking up and down!
    public bool isUpAcceptable, isDownAcceptable;
    
    //Initialize rotation speed for customization while tour runs. Two variables for inversion preference
    //Large numbers used to make sensitivity adjustments more gradual
    public float rotSpeedX = 120;
    public float rotSpeedY = 120;

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
            //z-axis is ALWAYS zero so the camera does not appear sideways at all.
            transform.rotation = isUpAcceptable
                ? Quaternion.Euler(xCamRot - rotSpeedX/100, yCamRot - rotSpeedY/100, 0)
                : Quaternion.Euler(xCamRot, yCamRot - rotSpeedY/100, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            transform.rotation = isDownAcceptable
                ? Quaternion.Euler(xCamRot + rotSpeedX/100, yCamRot - rotSpeedY/100, 0)
                : Quaternion.Euler(xCamRot, yCamRot - rotSpeedY/100, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = isUpAcceptable
                ? Quaternion.Euler(xCamRot - rotSpeedX/100, yCamRot + rotSpeedY/100, 0)
                : Quaternion.Euler(xCamRot, yCamRot + rotSpeedY/100, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = isDownAcceptable
                ? Quaternion.Euler(xCamRot + rotSpeedX/100, yCamRot + rotSpeedY/100, 0)
                : Quaternion.Euler(xCamRot, yCamRot + rotSpeedY/100, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.rotation = isUpAcceptable
                ? Quaternion.Euler(xCamRot - rotSpeedX/100, yCamRot, 0)
                : Quaternion.Euler(xCamRot, yCamRot, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(xCamRot, yCamRot - rotSpeedY/100, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.rotation = isDownAcceptable
                ? Quaternion.Euler(xCamRot + rotSpeedX/100, yCamRot, 0)
                : Quaternion.Euler(xCamRot, yCamRot, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(xCamRot, yCamRot + rotSpeedY/100, 0);
        }
        
        //I stands for invert - inverts the vertical camera rotation for user preference
        //GetKeyUP used to avoid multiple executions of key command
        else if (Input.GetKeyUp(KeyCode.I))
        {
            rotSpeedX *= -1;
        }
        
        //H stands for faster - camera rotation sensitivity is higher
        //GetKeyUP used to avoid multiple executions of key command
        else if (Input.GetKeyUp(KeyCode.H))
        {
            rotSpeedX += 20;
            rotSpeedY += 20;
        }
        
        //L stands for slower - camera rotation sensitivity is lower
        //GetKeyUP used to avoid multiple executions of key command
        else if (Input.GetKeyUp(KeyCode.L))
        {
            if (rotSpeedY > 0)
            {
                rotSpeedX -= 20;
                rotSpeedY -= 20;
            }
        }

        //R stands for reset - resets camera's position
        //GetKeyUP used to avoid multiple executions of key command
        else if (Input.GetKeyUp(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        //M stands for main menu - takes user back to main menu of the application
        else if (Input.GetKeyUp(KeyCode.M))
        {
            SceneManager.LoadScene("Menu1");
        }
        
        //Q stands for quit - closes out the entire application
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("Quitting the application");
            Application.Quit();
        }
    }
}