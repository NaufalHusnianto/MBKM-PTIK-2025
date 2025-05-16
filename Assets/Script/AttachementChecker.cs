using UnityEngine;

public class AttachmentChecker : MonoBehaviour
{
    public GameObject popupCanvas;

    public GameObject rayInteractorLeft;
    public GameObject rayInteractorRight;

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
            popupCanvas.SetActive(true);
            popupShown = true;

            rayInteractorLeft.SetActive(true);
            rayInteractorRight.SetActive(true);
        }
    }
}
