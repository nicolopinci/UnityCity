using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCity : MonoBehaviour
{
    // Start is called before the first frame update
    public System.Random ran = new System.Random();

    //Set these Textures in the Inspector
    public Texture m_MainTexture, m_Normal, m_Metal;
    Renderer m_Renderer;

    void Start()
    {


        // Create pavement
        GameObject pavement = GameObject.CreatePrimitive(PrimitiveType.Plane);
        pavement.transform.localScale = new Vector3(100, 100, 100);
        pavement.transform.position = new Vector3(0, 0, 0);

        // Create buildings
        for(int i=0; i<50; i=i+4)
        {
            for(int j = 0; j<50; j=j+4)
            {
                if(Mathf.Abs(i-50) > 10 && Mathf.Abs(j-50)>10)
                {
                    float width = ran.Next(1, 3);
                    float height = ran.Next(10, 50);
                    float length = ran.Next(1, 3);

                    int posX = i * 4 - 100;
                    float posY = height / 2;
                    int posZ = j * 4 - 100;

                    GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubeObj.transform.localScale = new Vector3(width, height, length);
                    cubeObj.transform.position = new Vector3(posX, posY, posZ);
                    cubeObj.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                }
               
            }
          
        }

        // Create landmark
       
        float pY = 50;

        GameObject landmark = GameObject.CreatePrimitive(PrimitiveType.Cube);
        landmark.name = "Landmark";
        landmark.transform.localScale = new Vector3(10, 100, 10);
        landmark.transform.position = new Vector3(0, pY, 0);
        landmark.GetComponent<Renderer>().material.color = new Color(0, 0, 255);


        // Create rectangle-shaped park in the scene

        GameObject park = GameObject.CreatePrimitive(PrimitiveType.Plane);
        park.transform.localScale = new Vector3(10, 10, 10);
        park.transform.position = new Vector3(100, 1, 0);


        // PARK TEXTURE
        //Make sure to enable the Keywords
        park.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");


        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        park.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture2D>("grass_main"));
        //Set the Normal map using the Texture you assign in the Inspector
        park.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture2D>("grass_normalMap"));
       
        // PAVEMENT TEXTURE
        //Make sure to enable the Keywords
        pavement.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
        pavement.GetComponent<Renderer>().material.EnableKeyword("_METALLICGLOSSMAP");


        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        pavement.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture2D>("pavement_diffuse"));
        //Set the Normal map using the Texture you assign in the Inspector
        pavement.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture2D>("pavement_normal"));
        //Set the Metallic Texture as a Texture you assign in the Inspector
        // m_Renderer.material.SetTexture("_MetallicGlossMap", m_Metal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
