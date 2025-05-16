using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachableObject : MonoBehaviour
{
    public float attachRadius = 0.3f;
    private XRGrabInteractable grab;
    private Rigidbody rb;
    private bool isGrabbed = false;

    private bool shouldCheckAttach = false;

    private Transform lastAttachPoint = null;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;

        // Lepas dari attach point jika sebelumnya menempel
        if (transform.parent != null && transform.parent.CompareTag("AttachPoint"))
        {
            transform.SetParent(null);
            lastAttachPoint = null;
        }

        rb.isKinematic = false;
        shouldCheckAttach = false;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        shouldCheckAttach = true;

        // Saat dilepas, sementara non-kinematic untuk bisa dilempar
        rb.isKinematic = false;
    }


    void Update()
    {
        if (!shouldCheckAttach || isGrabbed) return;

        AttachmentManager manager = FindObjectOfType<AttachmentManager>();
        if (manager == null) return;

        Transform point = manager.GetClosestAvailablePoint(transform.position, attachRadius);
        if (point != null)
        {
            // Menempel
            transform.position = point.position;
            transform.rotation = point.rotation;
            transform.SetParent(point);
            lastAttachPoint = point;

            rb.isKinematic = true;
        }
        else
        {
            // Tidak menempel, pastikan lepas dari parent
            if (transform.parent != null && transform.parent.CompareTag("AttachPoint"))
            {
                transform.SetParent(null);
                lastAttachPoint = null;
            }

            rb.isKinematic = false;
        }

        shouldCheckAttach = false;
    }


}
