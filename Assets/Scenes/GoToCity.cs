using UnityEngine;
using UnityEngine.UI;


public class GoToCity : MonoBehaviour
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
        goToCity();
    }

    void Destroy()
    {
        myselfButton.onClick.RemoveListener(() => simulateKeyPress());
    }

    void goToCity()
    {
        Vector3 position = new Vector3(146, 10, 29);
        Quaternion rot = Quaternion.Euler(new Vector3(0, -118.90f, 0));

        Camera.main.transform.SetPositionAndRotation(position, rot);
    }
}
