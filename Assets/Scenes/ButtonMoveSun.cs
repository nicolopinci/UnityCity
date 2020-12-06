using UnityEngine;
using UnityEngine.UI;

public class ButtonMoveSun : MonoBehaviour
{
    private Button myselfButton;

    void Start()
    {
        myselfButton = GetComponent<Button>();
        myselfButton.onClick.AddListener(() => simulateKeyPress());
    }

    void simulateKeyPress()
    {
        SunMovement.MoveSun(10);
    }

    void Destroy()
    {
        myselfButton.onClick.RemoveListener(() => simulateKeyPress());
    }
}