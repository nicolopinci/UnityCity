using UnityEngine;
using UnityEngine.UI;

public class ButtonZoomIn : MonoBehaviour
{
    private Button myselfButton;
    private float speed = 5000;

    void Start()
    {
        myselfButton = GetComponent<Button>();
        myselfButton.onClick.AddListener(() => zoomIn());
    }


    public void zoomIn()
    {
            Camera.main.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        
    }


}
