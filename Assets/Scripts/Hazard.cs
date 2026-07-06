using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private HazardData dataProfile;
    [SerializeField] private Transform KnockBackPoint;

    public HazardData Data => dataProfile;

    public Vector3 GetKnockBackPoint()
    {
        return KnockBackPoint != null? KnockBackPoint.position:Vector3.zero;
    }
}
