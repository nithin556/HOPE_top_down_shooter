using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private HazardData dataProfile;
    public HazardData Data => dataProfile;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageableTarget))
        {
            damageableTarget.invincibility_Time = dataProfile.invincibility_Time;
            damageableTarget.TakeDamage(dataProfile.damageAmount);
        }
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.MoveReduction(dataProfile.slow_Movement_Multiplier, dataProfile.slow_Movement_Duration);
        }
    }
}
