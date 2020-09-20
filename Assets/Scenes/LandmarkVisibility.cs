using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmarkVisibility : MonoBehaviour
{
    public GameObject cameraObj;
    public Camera visibilityCamera;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        cameraObj = new GameObject("Visibility camera");
        visibilityCamera = cameraObj.AddComponent<Camera>();

        mainCamera = Camera.main;
        mainCamera.enabled = true;
        visibilityCamera.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vector2 mousePosition = Input.mousePosition;
            
            GameObject landmark = GameObject.Find("Landmark");
            Vector3 positionLandmark = landmark.transform.position;

            Object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            List<GameObject> buildings = new List<GameObject>();

            foreach (Object o in allObjects)
            {
                if (o.name == "building")
                {
                    buildings.Add((GameObject)o);
                }
            }

            for (int i = 0; i < 360; i += 10)
            {
                for (int h = 0; h < landmark.transform.localScale.y/2; ++h)
                {
                    visibilityCamera.transform.position = new Vector3(positionLandmark.x, h + positionLandmark.y - landmark.transform.localScale.y/2, positionLandmark.z);
                    visibilityCamera.transform.localEulerAngles = new Vector3(0, i,  0);
                   

                    foreach (GameObject b in buildings)
                    {
                        if (IsTargetVisible(visibilityCamera, b))
                        {
                            b.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                            
                        }
                    }

                }
            }

            mainCamera.enabled = true;





            // source https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html?_ga=2.56937258.1826779449.1600550670-802618406.1600374693
            bool IsTargetVisible(Camera c, GameObject go)
            {
                var planes = GeometryUtility.CalculateFrustumPlanes(c);
                var point = go.transform.position;
                foreach (var plane in planes)
                {
                    if (plane.GetDistanceToPoint(point) < 0)
                        return false;
                }
                return true;
            }



        }
    }
}
    

