using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float rotateSpeed;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navMeshAgent.SetDestination(destination.position);
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation;
        if (navMeshAgent.velocity != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        }
        else
        {
            targetRotation = transform.rotation;
        }
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}
