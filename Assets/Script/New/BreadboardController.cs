using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BreadboardController : MonoBehaviour
{
    [System.Serializable]
    public class SequenceStep
    {
        public int order;
        public string expectedDevice;
    }

    public SequenceStep[] correctSequence;
    public GameObject successPopup;
    public GameObject rayInteractorLeft;
    public GameObject rayInteractorRight;

    public GameObject breadboard;
    public GameObject esp32;
    public GameObject dht22;
    public GameObject led;
    
    private bool[] sequenceStatus;
    private int correctCount = 0;

    private void Start()
    {
        sequenceStatus = new bool[correctSequence.Length];
        successPopup.SetActive(false);
        rayInteractorLeft.SetActive(false);
        rayInteractorRight.SetActive(false);
    }

    public void DeviceAttached(int order, string deviceType)
    {
        // Cari step yang sesuai dengan order
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (correctSequence[i].order == order)
            {
                bool isCorrect = (correctSequence[i].expectedDevice == deviceType);
                
                // Update status hanya jika berubah
                if (sequenceStatus[i] != isCorrect)
                {
                    sequenceStatus[i] = isCorrect;
                    correctCount += isCorrect ? 1 : -1;
                }
                break;
            }
        }
        
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        if (correctCount == correctSequence.Length)
        {
            ShowSuccessPopup();
            rayInteractorLeft.SetActive(true);
            rayInteractorRight.SetActive(true);

            breadboard.GetComponent<XRGrabInteractable>().enabled = false;
            esp32.GetComponent<XRGrabInteractable>().enabled = false;
            dht22.GetComponent<XRGrabInteractable>().enabled = false;
            led.GetComponent<XRGrabInteractable>().enabled = false;
        }
    }

    private void ShowSuccessPopup()
    {
        successPopup.SetActive(true);
        
        // Contoh: Sembunyikan setelah 3 detik
        // CancelInvoke("HideSuccessPopup");
        // Invoke("HideSuccessPopup", 3f);
    }

    private void HideSuccessPopup()
    {
        successPopup.SetActive(false);
    }

    // Untuk debug
    public void ResetBoard()
    {
        correctCount = 0;
        for (int i = 0; i < sequenceStatus.Length; i++)
        {
            sequenceStatus[i] = false;
        }
        successPopup.SetActive(false);
    }
}