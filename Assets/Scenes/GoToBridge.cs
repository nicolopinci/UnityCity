using UnityEngine;
using UnityEngine.UI;


public class GoToBridge : MonoBehaviour
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
        goToBridge();
    }

    void Destroy()
    {
        myselfButton.onClick.RemoveListener(() => simulateKeyPress());
    }

    void goToBridge()
    {
        Vector3 position = new Vector3(-275.2818f, 230, -1273.247f);
        Quaternion rot = Quaternion.Euler(new Vector3(0, 9.723f, 0));

        Camera.main.transform.SetPositionAndRotation(position, rot);
        Camera.main.fieldOfView = 65.82f;
    }
}
