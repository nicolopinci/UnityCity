using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Callbacks;
using System.Xml;

public class ImportScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            importFile();
        }
    }

    // Source: https://forum.unity.com/threads/vector3-from-string-solved.46223/
    public static Vector3 getVector3(string rString)
    {
        string[] temp = rString.Substring(1, rString.Length - 2).Split(',');
        float x = float.Parse(temp[0]);
        float y = float.Parse(temp[1]);
        float z = float.Parse(temp[2]);
        Vector3 rValue = new Vector3(x, y, z);
        return rValue;
    }


    public static Vector2 getVector2(string rString)
    {
        string[] temp = rString.Substring(1, rString.Length - 2).Split(',');
        float x = float.Parse(temp[0]);
        float y = float.Parse(temp[1]);
        Vector2 rValue = new Vector2(x, y);
        return rValue;
    }


    public static void importFile()
    {
        var path = EditorUtility.OpenFilePanel(
      "Load an existing scene",
      "",
      "xml");

        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        var allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
        foreach (GameObject o in allObjects)
        {
            if (o.name.StartsWith("building") || o.name.StartsWith("Landmark") || o.name.StartsWith("park") || o.name.StartsWith("Pavement"))
            {
                Destroy(o);
            }
        }

        Debug.Log(allObjects.Length);

        Debug.Log(doc.DocumentElement.ChildNodes.Count);

        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
            var primitive = PrimitiveType.Cube;

            if(node.Attributes["primitive"] ?.InnerText == "Plane")
            {
                primitive = PrimitiveType.Plane;
            }

            GameObject obj = GameObject.CreatePrimitive(primitive);
            obj.transform.localScale = getVector3(node.Attributes["localScale"]?.InnerText);
            obj.name = node.Attributes["name"]?.InnerText;
            obj.transform.position = getVector3(node.Attributes["position"]?.InnerText);
            obj.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
            obj.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture2D>(node.Attributes["MainTexture"]?.InnerText + "_diffuse"));
            obj.GetComponent<Renderer>().material.SetTextureScale("_MainTex", getVector2(node.Attributes["MainTexScale"]?.InnerText));
            obj.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture2D>(node.Attributes["BumpMap"]?.InnerText + "_normal"));
            obj.GetComponent<Renderer>().material.SetTextureScale("_BumpMap", getVector2(node.Attributes["BumpMapScale"]?.InnerText));

        }

        /*FileUtil.ReplaceFile(path, "Assets/Scenes/SavedSceneGenerator.cs");

        AssetDatabase.Refresh();*/

        //SunMovement.MoveSun(100);

        //Camera.main.gameObject.AddComponent(Type.GetType("SavedSceneGenerator"));






    }

   
}
