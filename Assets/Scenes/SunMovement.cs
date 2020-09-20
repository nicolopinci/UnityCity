using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public GameObject sun;

    // Sun creation
    void Start()
    {
        sun = new GameObject("Sun");

        Light light = sun.AddComponent<Light>();
        light.color = Color.yellow;
        light.transform.position = new Vector3(0, 0, 0);
        light.type = LightType.Directional;
        light.transform.localEulerAngles = new Vector3(30, 90, 0);
        light.shadows = LightShadows.Hard;

 

    }

    // Sun movement simulation
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            Light sunLight = sun.GetComponent<Light>();
            Vector3 currentAngles = sunLight.transform.localEulerAngles;

            sunLight.transform.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y + 10, currentAngles.z);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            Light sunLight = sun.GetComponent<Light>();
            Vector3 currentAngles = sunLight.transform.localEulerAngles;

            sunLight.transform.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y - 10, currentAngles.z);

        }
    }
}
