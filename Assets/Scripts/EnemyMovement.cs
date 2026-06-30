using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private NavMeshAgent navMeshAgent;
    private SpringDamperScript springDamperScript;

    void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }
    void Update()
    {
        navMeshAgent.SetDestination(destination.position);
    }
}
