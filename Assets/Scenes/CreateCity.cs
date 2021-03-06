﻿using System.Collections;
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

        /*
        // Create pavement
        GameObject pavement = GameObject.CreatePrimitive(PrimitiveType.Plane);
        pavement.name = "Pavement";
        pavement.transform.localScale = new Vector3(100, 100, 100);
        pavement.transform.position = new Vector3(0, 0, 0);
        */

        // Create buildings
        for(int i=0; i<50; i=i+4)
        {
            for(int j = 0; j<50; j=j+4)
            {
                if(Mathf.Abs(i-50) > 10 && Mathf.Abs(j-50)>10)
                {
                    float width = ran.Next(1, 5);
                    float height = ran.Next(10, 20);
                    float length = ran.Next(1, 5);

                    int posX = i * 4 - 100;
                    float posY = height / 2;
                    int posZ = j * 4 - 100;

                    GameObject cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cubeObj.transform.localScale = new Vector3(width, height, length);
                    cubeObj.transform.position = new Vector3(posX, posY, posZ);
                    //cubeObj.GetComponent<Renderer>().material.color = new Color(128, 128, 128);
                    cubeObj.name = "building_" + i + "_" + j;

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

        // Create landmark
       
        GameObject landmark = GameObject.CreatePrimitive(PrimitiveType.Cube);
        landmark.name = "Landmark";
        landmark.transform.localScale = new Vector3(5, 50, 5);
        landmark.transform.position = new Vector3(0, landmark.transform.localScale.y/2, 0);

    

        // Create rectangle-shaped park in the scene

        GameObject park = GameObject.CreatePrimitive(PrimitiveType.Plane);
        park.transform.localScale = new Vector3(10, 10, 10);
        park.transform.position = new Vector3(100, 1, 0);
        park.name = "park";


        // PARK TEXTURE
        //Make sure to enable the Keywords
        park.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");


        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        park.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture2D>("grass_diffuse"));
        park.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(10, 10));

        //Set the Normal map using the Texture you assign in the Inspector
        park.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture2D>("grass_normal"));
        park.GetComponent<Renderer>().material.SetTextureScale("_BumpMap", new Vector2(10, 10));

        /*
        // PAVEMENT TEXTURE
        //Make sure to enable the Keywords
        pavement.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");


        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        pavement.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture2D>("pavement_diffuse"));
        pavement.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(50, 50));
        
        //Set the Normal map using the Texture you assign in the Inspector
        pavement.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture2D>("pavement_normal"));
        pavement.GetComponent<Renderer>().material.SetTextureScale("_BumpMap", new Vector2(50, 50));
        */



        // LANDMARK TEXTURE
        //Make sure to enable the Keywords
        landmark.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");


        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        landmark.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture2D>("landmark_diffuse"));
        landmark.GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(3, 10));

        //Set the Normal map using the Texture you assign in the Inspector
        landmark.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture2D>("landmark_normal"));
        landmark.GetComponent<Renderer>().material.SetTextureScale("_BumpMap", new Vector2(3, 10));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
