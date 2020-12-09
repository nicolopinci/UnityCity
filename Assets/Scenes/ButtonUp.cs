using UnityEngine;
using UnityEngine.UI;

public class ButtonUp : MonoBehaviour
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
        CameraMovement.goUp(10);
    }

    void Destroy()
    {
        myselfButton.onClick.RemoveListener(() => simulateKeyPress());
    }
}
