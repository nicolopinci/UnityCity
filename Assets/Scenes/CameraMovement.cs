using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public float angularSpeed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 300;
        angularSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }

        if(Input.GetKey(KeyCode.R))
        {
            transform.Rotate(new Vector3(0, angularSpeed * Time.deltaTime,  0));
        }

        if (Input.GetKey(KeyCode.T))
        {
            transform.Rotate(new Vector3(0, -angularSpeed * Time.deltaTime,  0));
        }

        if (Input.GetKey(KeyCode.X))
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        }

        if(Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
        {
            changeFOV(-10);
        }

        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            changeFOV(10);
        }

    }

    static void changeFOV(float percentage)
    {
        float oldFOV = Camera.main.fieldOfView;
        Camera.main.fieldOfView = (1 + percentage / 100) * oldFOV;
    }

    public static void goUp(float amount)
    {
        Camera.main.transform.Translate(new Vector3(0, amount, 0));
    }

    public static void goDown(float amount)
    {
        Camera.main.transform.Translate(new Vector3(0, -amount, 0));
        if(Camera.main.transform.position.y < 0)
        {
            Vector3 oldPosition = Camera.main.transform.position;
            Vector3 newPosition = oldPosition;
            newPosition.y = 0;

            Quaternion oldRotation = Camera.main.transform.rotation;

            Camera.main.transform.SetPositionAndRotation(newPosition, oldRotation);
        }
    }
}
