using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
            computeHeatMaps();
        }


    }

    public static void computeHeatMaps()
    {
        EditorUtility.DisplayDialog("Heat map computation", "The computation of the heat maps has started. Please wait for the message that indicates the end of the computation.", "Ok");


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

        // var sequence = "10"; // sequence for CFH

        var sequence = GameObject.Find("CFH Sequence").GetComponent<InputField>().text;

        foreach(char c in sequence)
        {
            if(c != '0' && c != '1')
            {
                EditorUtility.DisplayDialog("Invalid CFH sequence", "Please use only 0's and 1's in the CFH sequence", "Ok");
                return;
            }
        }

        var shadowPoints = new Dictionary<Vector3, int>();
        var cfhPoints = new Dictionary<Vector3, string>();
        var cumulativeCFH = new Dictionary<Vector3, int>();


        Vector3 directionSun = sun.transform.forward;
        int alphaMax = 360;
        int step = 10;

        for (int alpha = 0; alpha < alphaMax; alpha+=step)
        {
            // Move sun
            sun.GetComponent<Light>().transform.Rotate(new Vector3(step, 0, 0));

            //sun.transform.localEulerAngles = new Vector3(sun.transform.localEulerAngles.x, alpha, sun.transform.localEulerAngles.z);

            Vector3 directionMovingSun = sun.transform.forward;

            for (int i = (int)minBound.x; i < (int)maxBound.x; ++i)
            {
                for (int j = (int)minBound.z; j < (int)maxBound.z; ++j)
                {
                    Vector3 currentOrigin = new Vector3(i, minBound.y, j);
                    directionMovingSun.Normalize();

                    if (Physics.Raycast(currentOrigin, -directionMovingSun, 10000)) // shadow
                    {
                        if (shadowPoints.ContainsKey(currentOrigin))
                        {
                            shadowPoints[currentOrigin] += 1;
                        }
                        else
                        {
                            shadowPoints[currentOrigin] = 1;
                        }

                        if (cfhPoints.ContainsKey(currentOrigin))
                        {
                            cfhPoints[currentOrigin] = System.String.Concat(cfhPoints[currentOrigin], "1");
                        }
                        else
                        {
                            cfhPoints[currentOrigin] = "1";
                        }
                    }
                    else // no shadow
                    {
                        if (cfhPoints.ContainsKey(currentOrigin))
                        {
                            cfhPoints[currentOrigin] = System.String.Concat(cfhPoints[currentOrigin], "0");
                        }
                        else
                        {
                            cfhPoints[currentOrigin] = "0";
                        }
                    }



                }
            }
        }

        foreach (KeyValuePair<Vector3, string> entry in cfhPoints)
        {
            cumulativeCFH[entry.Key] = Regex.Matches(entry.Value, sequence).Count;
        }


        var rows = (int)maxBound.x - (int)minBound.x;
        var columns = (int)maxBound.z - (int)minBound.z;

        int[,] matrixShadowMap = new int[rows, columns];
        int[,] matrixCFH = new int[rows, columns]; // the time is determined by the steps of the sun movement

        foreach (KeyValuePair<Vector3, int> entry in shadowPoints)
        {
            matrixShadowMap[(int)entry.Key.x - (int)minBound.x, (int)entry.Key.z - (int)minBound.z] = entry.Value;
        }

        foreach (KeyValuePair<Vector3, int> entry in cumulativeCFH)
        {
            matrixCFH[(int)entry.Key.x - (int)minBound.x, (int)entry.Key.z - (int)minBound.z] = entry.Value;
        }

        generateGraph(matrixShadowMap, "ShadowMap.html", rows, columns, "Shadow Map");
        generateGraph(matrixCFH, "CFH.html", rows, columns, "CFH");

        EditorUtility.DisplayDialog("Heat map computation", "The heat maps have been generated successfully. The results will be displayed in your default browser.", "Ok");

        Application.OpenURL("ShadowMap.html");
        Application.OpenURL("CFH.html");
    }

    static void generateGraph(int[,] matrix, string file_name, int rows, int columns, string title) {

        using (TextWriter tw = new StreamWriter(file_name))
        {
            tw.Write("<head>   <script src = 'https://cdn.plot.ly/plotly-latest.min.js' ></script></head><body><div id = 'myDiv' ></div>");

            if(title == "CFH")
            {
                tw.Write("<p>In the CFH plot, when the color tends towards blue, the searched sequence is less present, while when the colour tends to be red, the sequence is present a higher number of times.</p>");
            }
            else if(title == "Shadow Map")
            {
                tw.Write("<p>In the Shadow Map plot, when the color tends towards red, the Sun is less present, while when the colour tends to be blue, the Sun is present for a longer time.</p>");
            }

            tw.Write("</body><script> var data = [{z: [");
            for (int j = 0; j < rows; j++)
            {

                tw.Write("[");


                for (int i = 0; i < columns; i++)
                {
                    tw.Write(matrix[j, i]);
                    if (i != columns - 1)
                    {
                        tw.Write(", ");
                    }
                }
                if (j == rows - 1)
                {
                    tw.Write("]");
                }
                else
                {
                    tw.Write("], ");
                }

            }
            tw.Write("], type: 'heatmap'}]; Plotly.newPlot('myDiv', data, layout = {width: 700, height: 700, xaxis: {title: {text: 'x'}}, yaxis: {title: {text: 'z'}}, title: {text: '" + title + "'}});</script>");
        }


    }

    bool compatible(string observed, string sequence)
    {
        for(int i=0; i<observed.Length; ++i)
        {
            if(observed[i] != sequence[i])
            {
                return false;
            }
        }
        return true;
    }
}

     
 
