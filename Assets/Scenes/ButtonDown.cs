using UnityEngine;
using UnityEngine.UI;

public class ButtonDown : MonoBehaviour
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
        CameraMovement.goDown(10);
    }

    void Destroy()
    {
        myselfButton.onClick.RemoveListener(() => simulateKeyPress());
    }
}
