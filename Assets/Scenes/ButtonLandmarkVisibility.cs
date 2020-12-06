using UnityEngine;
using UnityEngine.UI;

public class ButtonLandmarkVisibility : MonoBehaviour
{
    private Button myselfButton;

    void Start()
    {
        myselfButton = GetComponent<Button>();
        myselfButton.onClick.AddListener(() => simulateKeyPress());
    }

    void simulateKeyPress()
    {
        LandmarkVisibility.computeLandmarkVisibility();
    }

    void Destroy()
    {
        myselfButton.onClick.RemoveListener(() => simulateKeyPress());
    }
}
