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
