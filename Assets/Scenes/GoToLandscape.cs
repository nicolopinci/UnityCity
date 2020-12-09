using UnityEngine;
using UnityEngine.UI;


public class GoToLandscape : MonoBehaviour
{
    private Button myselfButton;

    // Start is called before the first frame update
    void Start()
    {
        myselfButton = GetComponent<Button>();
        myselfButton.onClick.AddListener(() => simulateKeyPress());
    }

    void simulateKeyPress()
    {
        goToLandscape();
    }

    void Destroy()
    {
        myselfButton.onClick.RemoveListener(() => simulateKeyPress());
    }

    void goToLandscape()
    {
        Vector3 position = new Vector3(1705, 650, 301);
        Quaternion rot = Quaternion.Euler(new Vector3(0, -96.67f, 0));

        Camera.main.transform.SetPositionAndRotation(position, rot);
        Camera.main.fieldOfView = 65.82f;
    }
}
