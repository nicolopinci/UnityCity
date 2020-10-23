using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditRemove : MonoBehaviour
{
    bool dragging = false;
    Vector3 source = new Vector3();
    Vector3 offset = new Vector3();
    bool handleUp = false;
    GameObject clicked_object;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            LeftClick(ref source, ref dragging, ref offset, ref handleUp, ref clicked_object);
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        if(dragging)
        {
            var destinationSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, source.z);
            var destination = Camera.main.ScreenToWorldPoint(destinationSpace) + offset;
            clicked_object.transform.position = destination;
        }

        if (Input.GetMouseButtonDown(1))
        {
            RightClick();
        }
    }

    void RightClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            var clicked_object = hit.collider.gameObject;
            
            // Right click on building/landmark -> remove building or landmark
            if(clicked_object.name.StartsWith("building") || clicked_object.name.StartsWith("Landmark"))
            {
                Destroy(clicked_object);
            }
            // Right click on Plane -> calculate sky visibility
            else
            {
                var origin = hit.point;
                var point_visibility = 0;
                var tot_points = 0;

                for (var a = 0; a < 180; a++)
                {
                    for(var b = 0; b < 360; b++)
                    {
                        var a_r = (Mathf.PI * a) / 180;
                        var b_r = (Mathf.PI * b) / 180;

                        Vector3 direction = new Vector3(Mathf.Sin(a_r) * Mathf.Cos(b_r), Mathf.Cos(a_r), Mathf.Sin(a_r) * Mathf.Sin(b_r));
                        var sky_ray = new Ray(origin, direction);

                        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

                        var num_intersections = 0;

                        foreach(GameObject o in allObjects) {
                            RaycastHit ob_hit;

                            var obj_collider = o.GetComponent<Collider>();

                            if (obj_collider != null)
                            {
                                if (obj_collider.Raycast(sky_ray, out ob_hit, 1000.0f))
                                {
                                    num_intersections += 1;
                                }
                            }
                        }

                        if(num_intersections > 0)
                        {
                            point_visibility += 1;
                        }
                        tot_points += 1;
                    }
                }

                point_visibility = tot_points - point_visibility;

                Debug.Log("Sky visibility proportion: " + (float)point_visibility / (float)tot_points);
            }


        }
    }


    void LeftClick(ref Vector3 source, ref bool dragging, ref Vector3 offset, ref bool handleUp, ref GameObject clicked_object)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            clicked_object = hit.collider.gameObject;

            // Left click on building/landmark -> drag building or landmark
            if (clicked_object.name.StartsWith("building") || clicked_object.name.StartsWith("Landmark"))
            {
                handleUp = true;
                dragging = true;
                source = Camera.main.WorldToScreenPoint(clicked_object.transform.position);
                offset = clicked_object.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, source.z));
            }
          
        }

    }

}
