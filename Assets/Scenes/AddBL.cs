using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBL : MonoBehaviour
{
    // Start is called before the first frame update
    public System.Random ran = new System.Random();

    //Set these Textures in the Inspector
    public Texture m_MainTexture, m_Normal, m_Metal;
    Renderer m_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Add building
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                var hovered_object = hit.collider.gameObject;

                // Left click on building/landmark -> drag building or landmark
                if (!hovered_object.name.StartsWith("building") && !hovered_object.name.StartsWith("Landmark"))
                {
                    
                    var mouse_position = hit.point;


                    // Create building 

                    float width = ran.Next(1, 5);
                    float height = ran.Next(10, 20);
                    float length = ran.Next(1, 5);


                    GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubeObj.transform.localScale = new Vector3(width, height, length);
                    mouse_position = mouse_position + new Vector3(0, height / 2, 0);
                    cubeObj.transform.position = mouse_position;
                    //cubeObj.GetComponent<Renderer>().material.color = new Color(128, 128, 128);
                    cubeObj.name = "building_" + mouse_position.x + "_" + mouse_position.z;

                    // BUILDING TEXTURE
                    //Make sure to enable the Keywords
                    cubeObj.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
                    //cubeObj.GetComponent<Renderer>().material.EnableKeyword("_METALLICGLOSSMAP");


                    //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
                    cubeObj.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture2D>("building_diffuse"));
                    cubeObj.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(3, 10));

                    //Set the Normal map using the Texture you assign in the Inspector
                    cubeObj.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture2D>("building_normal"));
                    cubeObj.GetComponent<Renderer>().material.SetTextureScale("_BumpMap", new Vector2(3, 10));


                }

            }
        }

        if (Input.GetKeyDown(KeyCode.L)) // Add building
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                var hovered_object = hit.collider.gameObject;

                // Left click on building/landmark -> drag building or landmark
                if (!hovered_object.name.StartsWith("building") && !hovered_object.name.StartsWith("Landmark"))
                {

                    var mouse_position = hit.point;


                    // Create landmark 

                    float width = ran.Next(5, 10);
                    float height = ran.Next(20, 40);
                    float length = ran.Next(5, 10);


                    GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubeObj.transform.localScale = new Vector3(width, height, length);
                    mouse_position = mouse_position + new Vector3(0, height / 2, 0);
                    cubeObj.transform.position = mouse_position;
                    //cubeObj.GetComponent<Renderer>().material.color = new Color(128, 128, 128);
                    cubeObj.name = "Landmark_" + mouse_position.x + "_" + mouse_position.z;

                    // BUILDING TEXTURE
                    //Make sure to enable the Keywords
                    cubeObj.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
                    //cubeObj.GetComponent<Renderer>().material.EnableKeyword("_METALLICGLOSSMAP");


                    //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
                    cubeObj.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture2D>("landmark_diffuse"));
                    cubeObj.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(3, 10));

                    //Set the Normal map using the Texture you assign in the Inspector
                    cubeObj.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture2D>("landmark_normal"));
                    cubeObj.GetComponent<Renderer>().material.SetTextureScale("_BumpMap", new Vector2(3, 10));


                }

            }
        }

    }
}
