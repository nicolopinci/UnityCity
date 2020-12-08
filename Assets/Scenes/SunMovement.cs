using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public static GameObject sun;
    bool keepMoving = false;

    // Sun creation
    void Start()
    {
        sun = new GameObject("Sun");

        Light light = sun.AddComponent<Light>();
        light.color = Color.white;
        light.transform.position = new Vector3(0, 0, 0);
        light.type = LightType.Directional;
        light.transform.localEulerAngles = new Vector3(30, 90, 0);
        light.shadows = LightShadows.Hard;

 

    }

    // Sun movement simulation
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            keepMoving = !keepMoving;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            MoveSun(-10);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            MoveSun(10);
        }

        if (keepMoving)
        {
            MoveSun(-1);

        }
        
    }

    public static void MoveSun(float amount)
    {
        GameObject sun = GameObject.Find("Sun");

        Light sunLight = sun.GetComponent<Light>();
        Vector3 currentAngles = sunLight.transform.localEulerAngles;

        sunLight.transform.Rotate(new Vector3(amount, 0, 0));


        // Update bars

        Object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));


        var sunDirection = sun.GetComponent<Light>().transform.localEulerAngles.x;

        Vector3 directionSun = sun.transform.forward;

        foreach (Object o in allObjects)
        {
            if (o.name.StartsWith("animatedbar"))
            {
                var old_name = o.name;
                var point = parseName(o.name);
                var originalHeight = int.Parse(o.name.Split('_')[2]);

                Destroy(o);
                var height = AnimatedBar.calculateHeight(point);
                
                GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeObj.transform.localScale = new Vector3(4, height, 4);
                point = point + new Vector3(0, height / 2, 0) + new Vector3(0, -originalHeight, 0);
                cubeObj.transform.position = point;
                var bar_renderer = cubeObj.GetComponent<Renderer>();

                Color col = Color.red;
                col.r = height/100.0f;
                col.a = 0.5f;


                bar_renderer.material.SetColor("_Color", col);

                
                //cubeObj.GetComponent<Renderer>().material.color = new Color(128, 128, 128);
                cubeObj.name = old_name;



            }
        }
    }

    public static Vector3 parseName(string bar_name)
    {

        var x = (float)System.Decimal.Parse(bar_name.Split('_')[1]);
        var y = (float)System.Decimal.Parse(bar_name.Split('_')[2]);
        var z = (float)System.Decimal.Parse(bar_name.Split('_')[3]);

        Debug.Log(bar_name);
        Debug.Log(z);
        Debug.Log(x);
        Debug.Log(y);

        return new Vector3(x, y, z);
    }

}
