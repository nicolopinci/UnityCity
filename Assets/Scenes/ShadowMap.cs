using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShadowMap : MonoBehaviour { 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            Object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            List<GameObject> buildings = new List<GameObject>();

            foreach (Object o in allObjects)
            {
                if (o.name.StartsWith("building"))
                {
                    buildings.Add((GameObject)o);
                }
            }

            GameObject park = GameObject.Find("park");
            GameObject sun = GameObject.Find("Sun");

            Vector3 maxBound = park.GetComponent<Renderer>().bounds.max;
            Vector3 minBound = park.GetComponent<Renderer>().bounds.min;


            var shadowPoints = new Dictionary<Vector3, int>();

            for (int i = (int)minBound.x; i < (int)maxBound.x; ++i)
            {
                for (int j = (int)minBound.z; j < (int)maxBound.z; ++j)
                {
                    Vector3 currentOrigin = new Vector3(i, minBound.y, j);
                    Vector3 directionSun = sun.transform.forward;
                    directionSun.Normalize();
                    directionSun *= 100;




                    if (Physics.Raycast(currentOrigin, currentOrigin - directionSun, 1000))
                    {
                        if (shadowPoints.ContainsKey(currentOrigin))
                        {
                            shadowPoints[currentOrigin] += 1;
                        }
                        else
                        {
                            shadowPoints[currentOrigin] = 1;
                        }
                    }






                }
            }


            var rows = (int)maxBound.x - (int)minBound.x;
            var columns = (int)maxBound.z - (int)minBound.z;

            int[,] matrixShadowMap = new int[rows, columns];

            foreach (KeyValuePair<Vector3, int> entry in shadowPoints)
            {
                matrixShadowMap[(int)entry.Key.x, (int)entry.Key.y] = entry.Value;
            }


            using (TextWriter tw = new StreamWriter("computedShadowMap.txt"))
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        tw.Write(matrixShadowMap[i, j] + "\t");
                    }
                    tw.WriteLine();
                }
            }

        }
    }
}
