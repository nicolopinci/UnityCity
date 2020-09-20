using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmarkVisibility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vector2 mousePosition = Input.mousePosition;
            try
            {
                GameObject landmark = GameObject.Find("Landmark");
                if (landmark.GetComponent<Renderer>().isVisible)
                {
                    Debug.Log("The landmark is visible");
                }
                else
                {
                    Debug.Log("The landmark is not visible from this point");
                }
            }
            catch
            {
                Debug.Log("No landmark found");
            }

            
        }
    }
}
