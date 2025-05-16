using UnityEngine;

public class AttachmentManager : MonoBehaviour
{
    [Header("Attach Points (maksimal 3)")]
    public Transform[] attachmentPoints;

    public Transform GetAvailablePoint()
    {
        foreach (Transform point in attachmentPoints)
        {
            if (point.childCount == 0) return point;
        }
        return null;
    }

    public Transform GetClosestAvailablePoint(Vector3 objPos, float radius)
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (Transform point in attachmentPoints)
        {
            if (point.childCount > 0) continue;

            float dist = Vector3.Distance(objPos, point.position);
            if (dist < radius && dist < minDist)
            {
                minDist = dist;
                closest = point;
            }
        }

        return closest;
    }
}
