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
            Object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            List<GameObject> buildings = new List<GameObject>();
            List<GameObject> landmarks = new List<GameObject>();

            foreach (GameObject o in allObjects)
            {
                if (o.name.StartsWith("building"))
                {
                    buildings.Add((GameObject)o);
                    o.GetComponent<Renderer>().material.color = Color.white;

                }
                else if(o.name.StartsWith("Landmark"))
                {
                    landmarks.Add((GameObject)o);
                }
            }


            //GameObject landmark = GameObject.Find("Landmark");

            foreach (var landmark in landmarks)
            {
                Vector3 positionLandmark = landmark.transform.position;



                

                for (int i = 0; i < 360; i += 1)
                {
                    for (int h = 0; h < landmark.transform.localScale.y / 2; ++h)
                    {

                        visibilityCamera.transform.position = new Vector3(positionLandmark.x, h + positionLandmark.y - landmark.transform.localScale.y / 2, positionLandmark.z);
                        visibilityCamera.transform.localEulerAngles = new Vector3(0, i, 0);


                        foreach (GameObject b in buildings)
                        {
                            if (IsInView(landmark, b, visibilityCamera))
                            {
                                b.GetComponent<Renderer>().material.color = new Color(255, 0, 0);

                            }
                        }

                    }
                }
            }

        }
    }

    // Adapted from https://answers.unity.com/questions/8003/how-can-i-know-if-a-gameobject-is-seen-by-a-partic.html?_ga=2.78087828.1826779449.1600550670-802618406.1600374693
    private bool IsInView(GameObject origin, GameObject building, Camera cam)
    {
        Vector3 pointOnScreen = cam.WorldToScreenPoint(building.transform.position);

        //Is in front
        if (pointOnScreen.z < 0)
        {
            return false;
        }

        //Is in FOV
        if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
                (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
        {
            return false;
        }

        RaycastHit hit;
        Vector3 heading = building.transform.position - origin.transform.position;
        Vector3 direction = heading.normalized;// / heading.magnitude;

        Vector3 positionOnAx = building.transform.position;

        positionOnAx.y = cam.transform.position.y;

        if(positionOnAx.y > building.transform.localScale.y/2)
        {
            return false; // According to the slides, I assume that the view is only planar
        }

        if (Physics.Linecast(cam.transform.position, positionOnAx, out hit))
        {
            if (hit.transform.name != building.name)
            {
                return false;
            }
        }
        return true;
    }

}


