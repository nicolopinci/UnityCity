using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class ExportScene : MonoBehaviour
{
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {

            var preliminary_lines = new List<string>();

            preliminary_lines.Add("using System.Collections;");
            preliminary_lines.Add("using System.Collections.Generic;");
            preliminary_lines.Add("using UnityEngine;");

            preliminary_lines.Add("public class SavedSceneGenerator : MonoBehaviour {");
            preliminary_lines.Add("public Texture m_MainTexture, m_Normal;");
            preliminary_lines.Add("Renderer m_Renderer;");

            preliminary_lines.Add("void Update() { ");
            preliminary_lines.Add("if(Input.GetKeyDown(KeyCode.J)) {");

            preliminary_lines.Add("Object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));");

            preliminary_lines.Add("foreach (GameObject o in allObjects)");
            preliminary_lines.Add("{");
            preliminary_lines.Add("if (o.name.StartsWith(\"building\") || o.name.StartsWith(\"Landmark\") || o.name.StartsWith(\"park\") || o.name.StartsWith(\"Pavement\"))");
            preliminary_lines.Add("{");
            preliminary_lines.Add("Destroy(o);");
            preliminary_lines.Add("}}");



            File.WriteAllLines("Assets/Scenes/SavedSceneGenerator.cs", preliminary_lines);


            

            Object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            List<GameObject> buildings = new List<GameObject>();

            var index = 0;



            foreach (GameObject o in allObjects)
            {
                index += 1;
                if (o.name.StartsWith("building") || o.name.StartsWith("Landmark") || o.name.StartsWith("park") || o.name.StartsWith("Pavement")) 
                {
                    var current_array = new List<string>();
                    var primitive_type = "Plane";

                    if (o.name.StartsWith("building") || o.name.StartsWith("Landmark"))
                    {
                        primitive_type = "Cube";
                    }
                  

                    current_array.Add("GameObject obj_" + index.ToString() + " = GameObject.CreatePrimitive(PrimitiveType." + primitive_type + ");");

                    current_array.Add(" obj_" + index.ToString() + ".name = \"" + o.name + "\";");
                    current_array.Add(" obj_" + index.ToString() + ".transform.localScale = new Vector3" + o.transform.localScale.ToString("F0") + ";");
                    current_array.Add(" obj_" + index.ToString() + ".transform.position = new Vector3" + o.transform.position.ToString("F0") + ";");
                    current_array.Add(" obj_" + index.ToString() + ".GetComponent<Renderer>().material.EnableKeyword(\"_NORMALMAP\");");

                    var string_name = "building";
                    var scale_vec = "Vector3(3, 10)";

                    if(o.name.StartsWith("Landmark"))
                    {
                        string_name = "landmark";
                    }
                    else if(o.name.StartsWith("Pavement"))
                    {
                        string_name = "pavement";
                        scale_vec = "Vector3(50, 50)";
                    }
                    else if(o.name.StartsWith("park"))
                    {
                        string_name = "grass";
                        scale_vec = "Vector3(10, 10)";

                    }

                    current_array.Add(" obj_" + index.ToString() + ".GetComponent<Renderer>().material.SetTexture(\"_MainTex\", Resources.Load<Texture2D>(\"" + string_name + "_diffuse\"));");
                    current_array.Add(" obj_" + index.ToString() + ".GetComponent<Renderer>().material.SetTextureScale(\"_MainTex\", new " + scale_vec + ");");
                    current_array.Add(" obj_" + index.ToString() + ".GetComponent<Renderer>().material.SetTexture(\"_BumpMap\", Resources.Load<Texture2D>(\"" + string_name + "_normal\"));");
                    current_array.Add(" obj_" + index.ToString() + ".GetComponent<Renderer>().material.SetTextureScale(\"_BumpMap\", new " + scale_vec + ");");

       
                    File.AppendAllLines("Assets/Scenes/SavedSceneGenerator.cs", current_array);

                }
                

               
            }

            var final_lines = new List<string>();


            final_lines.Add("}}}");

            File.AppendAllLines("Assets/Scenes/SavedSceneGenerator.cs", final_lines);


        }
    }
   
}