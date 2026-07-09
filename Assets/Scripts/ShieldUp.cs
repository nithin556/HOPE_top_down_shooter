using UnityEngine;

public class ShieldUp : MonoBehaviour
{
    [SerializeField] private float shield_Duration;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Shield(shield_Duration);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("IDamageable not found on hit object");
        }
    }
}
