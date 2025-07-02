using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class XRDeviceAttacher : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    [Header("Device Settings")]
    public string deviceType; // "ESP32", "LED", "DHTSensor"

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;

        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Cari attach point terdekat yang valid
        XRAttachPoint nearestAttachPoint = FindNearestValidAttachPoint();

        if (nearestAttachPoint != null && nearestAttachPoint.CanAttach(deviceType))
        {
            AttachToPoint(nearestAttachPoint.transform);
            nearestAttachPoint.AttachDevice(this);
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    private XRAttachPoint FindNearestValidAttachPoint()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            XRAttachPoint attachPoint = hitCollider.GetComponent<XRAttachPoint>();
            if (attachPoint != null && attachPoint.CanAttach(deviceType))
            {
                return attachPoint;
            }
        }
        return null;
    }

    private void AttachToPoint(Transform attachPoint)
    {
        transform.SetParent(attachPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        
        // Nonaktifkan grab setelah terpasang
        grabInteractable.enabled = false;
        
        // Tambahkan rigidbody jika belum ada dan set ke kinematic
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void ReturnToOriginalPosition()
    {
        transform.SetParent(originalParent);
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
        
        // Aktifkan kembali grab
        grabInteractable.enabled = true;
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;
    }
}