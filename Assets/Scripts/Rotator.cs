using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180.0f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rotationSpeed = rotationSpeed * Mathf.Deg2Rad;
    }

    void FixedUpdate()
    {
        rb.angularVelocity = Vector3.up * rotationSpeed;
    }
}
