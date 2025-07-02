using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private BreadboardController breadboardController;

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ResetAllDevices()
    {
        // Reset semua perangkat IoT
        foreach (var device in FindObjectsOfType<XRDeviceAttacher>())
        {
            device.ReturnToOriginalPosition();
        }
        
        // Reset status breadboard
        if (breadboardController != null)
        {
            breadboardController.ResetBoard();
        }
        
        Debug.Log("All devices and breadboard have been reset");
    }
}
