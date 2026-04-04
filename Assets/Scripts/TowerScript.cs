using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] public float towerRadius;
    [SerializeField] float maxTowerHealth ;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawWireSphere(transform.position, towerRadius);
    }
}
