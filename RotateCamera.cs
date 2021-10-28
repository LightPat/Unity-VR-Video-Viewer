using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ArrowInput : MonoBehaviour
{
    // Update is called once per frame
    public float speed = 2.1f;
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, speed, 0.0000001f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -speed, -0.0000001f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(new Vector3(speed,0,-0.0000001f));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(-speed,0, 0.0000001f));
        }

        if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
