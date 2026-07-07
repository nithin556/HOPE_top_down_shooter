using UnityEngine;

public class Rotator : MonoBehaviour
{

    private Vector3 knockBackPoint;
    private float rotationSpeed;
    private float knockBackLerp_Seconds;
    private Rigidbody rb;
    private Hazard hazard;

    void Start()
    {
        hazard = GetComponent<Hazard>();
        rotationSpeed = hazard.Data.rotationSpeed;

        rb = GetComponent<Rigidbody>();
        rotationSpeed = rotationSpeed * Mathf.Deg2Rad;

    }

    void FixedUpdate()
    {
        rb.angularVelocity = Vector3.up * rotationSpeed;
    }
}
