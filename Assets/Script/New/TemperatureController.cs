using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class TemperatureController : MonoBehaviour
{
    [System.Serializable]
    public class AttachPoint
    {
        public Transform pointTransform;
        public float temperature;
        public bool hasBeenTried = false;
    }

    // public float hapticAmplitude = 0.5f;
    // public float hapticDuration = 0.1f;
    // public XRBaseController rightController;
    // public XRBaseController leftController;
    public GameObject rayInteractorLeft;
    public GameObject rayInteractorRight;

    public AttachPoint[] attachPoints;
    public TextMeshProUGUI temperatureText;
    public GameObject successCanvas;
    
    private XRGrabInteractable grabInteractable;
    private bool allPointsTried = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        UpdateTemperatureDisplay("Drag to measure");
        rayInteractorLeft.SetActive(false);
        rayInteractorRight.SetActive(false);
    }

    void Update()
    {
        if (!grabInteractable.isSelected && !allPointsTried)
        {
            CheckProximityToAttachPoints();
        }
    }

    void CheckProximityToAttachPoints()
    {
        foreach (var point in attachPoints)
        {
            float distance = Vector3.Distance(transform.position, point.pointTransform.position);
            
            if (distance < 0.5f) // Adjust threshold as needed
            {
                // rightController.SendHapticImpulse(hapticAmplitude, hapticDuration);
                // leftController.SendHapticImpulse(hapticAmplitude, hapticDuration);
                point.hasBeenTried = true;
                UpdateTemperatureDisplay(point.temperature.ToString("F1") + "°C");
                CheckAllPointsTried();
                return;
            }
        }
        
        UpdateTemperatureDisplay("--");
    }

    void CheckAllPointsTried()
    {
        foreach (var point in attachPoints)
        {
            if (!point.hasBeenTried) return;
        }

        allPointsTried = true;
        successCanvas.SetActive(true);
        rayInteractorLeft.SetActive(true);
        rayInteractorRight.SetActive(true);
    }

    void UpdateTemperatureDisplay(string text)
    {
        if (temperatureText != null)
        {
            temperatureText.text = text;
        }
    }
}