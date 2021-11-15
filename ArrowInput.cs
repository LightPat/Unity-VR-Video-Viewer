using System;
using UnityEngine;
public class ArrowInput : MonoBehaviour
{
    // Update is called once per frame
    public float xCamRot;
    public float yCamRot;
    
    private void Update()
    {
        xCamRot = transform.eulerAngles.x;
        yCamRot = transform.eulerAngles.y;
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (xCamRot <= -86 && xCamRot > -90)
                {
                    transform.rotation = Quaternion.Euler(xCamRot - 2, yCamRot + 2, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(xCamRot, yCamRot + 2, 0);
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (xCamRot >= 86 && xCamRot < 90)
                {
                    transform.rotation = Quaternion.Euler(xCamRot, yCamRot + 2, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(xCamRot + 2, yCamRot + 2, 0);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(xCamRot, yCamRot + 2, 0);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (xCamRot <= -86 && xCamRot > -90)
                {
                    transform.rotation = Quaternion.Euler(xCamRot, yCamRot - 2, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(xCamRot - 2, yCamRot - 2, 0);
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (xCamRot >= 86 && xCamRot < 90)
                {
                    transform.rotation = Quaternion.Euler(xCamRot, yCamRot - 2, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(xCamRot + 2, yCamRot - 2, 0);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(xCamRot, yCamRot - 2, 0);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (xCamRot >= 86 && xCamRot < 90)
                {
                    transform.rotation = Quaternion.Euler(xCamRot, yCamRot + 2, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(xCamRot + 2, yCamRot + 2, 0);
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (xCamRot >= 86 && xCamRot < 90)
                {
                    transform.rotation = Quaternion.Euler(xCamRot, yCamRot - 2, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(xCamRot + 2, yCamRot - 2, 0);
                }
            }
            else
            {
                if (xCamRot >= -86)
                {
                    if (xCamRot >= 86 && xCamRot < 90)
                    {
                        transform.rotation = Quaternion.Euler(xCamRot, yCamRot, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(xCamRot + 2, yCamRot, 0);   
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (xCamRot >= -90 && xCamRot < -84)
                {
                    transform.rotation = Quaternion.Euler(xCamRot, yCamRot + 2, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(xCamRot - 2, yCamRot + 2, 0);
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (xCamRot >= -90 && xCamRot < -84)
                {
                    transform.rotation = Quaternion.Euler(xCamRot, yCamRot - 2, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(xCamRot - 2, yCamRot - 2, 0);
                }
            }
            else
            {
                //float rotateX = Mathf.Clamp(xCamRot - 2, -86, 86);
                //transform.eulerAngles = new Vector3(rotateX, yCamRot, 0);
                
                if (xCamRot >= -90)
                {
                   if (xCamRot > -84)
                   {
                        transform.rotation = Quaternion.Euler(xCamRot - 2, yCamRot, 0);
                   }
                   else
                   {
                       transform.rotation = Quaternion.Euler(xCamRot, yCamRot, 0);
                   }
                }
            }
        }
    

        //N stands for Next scene - looks at interactible for next scene
        if (Input.GetKey(KeyCode.N))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        //M stands for Main Menu - looks at interactible for main menu
        if (Input.GetKey(KeyCode.M))
        {
            transform.rotation = Quaternion.Euler(80, 0, 0);
        }
    }
}
