using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimatedBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N)) // Add animated bar
        {
            createBar();
        }

    }

    void createBar()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            var hovered_object = hit.collider.gameObject;

            if (!hovered_object.name.StartsWith("building") && !hovered_object.name.StartsWith("Landmark"))
            {

                var mouse_position = hit.point;

                var height = calculateHeight(mouse_position);

                GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeObj.transform.localScale = new Vector3(4, height, 4);
                mouse_position = mouse_position + new Vector3(0, height / 2, 0);
                mouse_position = Vector3Int.RoundToInt(mouse_position);

                cubeObj.transform.position = mouse_position;

                var bar_renderer = cubeObj.GetComponent<Renderer>();
                cubeObj.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                //cubeObj.GetComponent<PhysicsRaycaster>().enabled = false;

                Color col = Color.red;
                col.r = height/100.0f;
                col.a = 0.5f;

                bar_renderer.material.SetColor("_Color", col);


                //cubeObj.GetComponent<Renderer>().material.color = new Color(128, 128, 128);
                cubeObj.name = "animatedbar_" + mouse_position.x + "_" + mouse_position.y + "_" + mouse_position.z;



            }

        }
    }


    public static float calculateHeight(Vector3 point)
    {

        GameObject sun = GameObject.Find("Sun");

        var sunDirection = sun.GetComponent<Light>().transform.localEulerAngles.x;

        Vector3 directionSun = sun.transform.forward;

        if (Physics.Raycast(point, -directionSun, 10000)) {
            return 0.0f;
        }

        return 100.0f*Mathf.Max(0.0f, Mathf.Sin(sunDirection/180*Mathf.PI));
    }
}
