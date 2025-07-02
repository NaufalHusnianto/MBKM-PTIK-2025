using UnityEngine;

public class XRAttachPoint : MonoBehaviour
{
    [Header("Attachment Settings")]
    public string expectedDeviceType;
    public int sequenceOrder = -1; // -1 untuk tidak perlu urutan spesifik
    
    [Header("Visual Feedback")]
    public Material highlightMaterial;
    private Material originalMaterial;
    private MeshRenderer meshRenderer;

    private BreadboardController breadboardController;
    private bool isOccupied = false;

    private void Start()
    {
        breadboardController = GetComponentInParent<BreadboardController>();
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer) originalMaterial = meshRenderer.material;
    }

    public bool CanAttach(string deviceType)
    {
        return !isOccupied && deviceType == expectedDeviceType;
    }

    public void AttachDevice(XRDeviceAttacher device)
    {
        isOccupied = true;
        if (breadboardController != null && sequenceOrder >= 0)
        {
            breadboardController.DeviceAttached(sequenceOrder, device.deviceType);
        }
        
        // Update visual
        if (meshRenderer) meshRenderer.enabled = false;
    }

    public void DetachDevice()
    {
        isOccupied = false;
        if (meshRenderer) meshRenderer.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        XRDeviceAttacher device = other.GetComponent<XRDeviceAttacher>();
        if (device != null && CanAttach(device.deviceType) && meshRenderer)
        {
            meshRenderer.material = highlightMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (meshRenderer && originalMaterial != null)
        {
            meshRenderer.material = originalMaterial;
        }
    }
}