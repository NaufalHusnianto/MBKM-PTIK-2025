using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachmentChecker : MonoBehaviour
{
    public GameObject popupCanvas;

    public GameObject rayInteractorLeft;
    public GameObject rayInteractorRight;

    public GameObject breadboard;
    public GameObject esp32;
    public GameObject dht22;
    public GameObject led;

    public Transform point1;
    public Transform point2;
    public Transform point3;

    public Transform object1;
    public Transform object2;
    public Transform object3;

    private bool popupShown = false;

    void Start()
    {
        popupCanvas.SetActive(false);
        rayInteractorLeft.SetActive(false);
        rayInteractorRight.SetActive(false);
    }

    void Update()
    {
        if (popupShown) return;

        bool obj1Correct = object1.parent == point1;
        bool obj2Correct = object2.parent == point2;
        bool obj3Correct = object3.parent == point3;

        if (obj1Correct && obj2Correct && obj3Correct)
        {
            breadboard.GetComponent<XRGrabInteractable>().enabled = false;
            esp32.GetComponent<XRGrabInteractable>().enabled = false;
            dht22.GetComponent<XRGrabInteractable>().enabled = false;
            led.GetComponent<XRGrabInteractable>().enabled = false;

            popupCanvas.SetActive(true);
            popupShown = true;

            rayInteractorLeft.SetActive(true);
            rayInteractorRight.SetActive(true);
        }
    }
}
