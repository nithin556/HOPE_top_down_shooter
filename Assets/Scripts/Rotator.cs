using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(0f,rotationSpeed*Time.fixedDeltaTime,0f);
        Quaternion targetRotation = rb.rotation * deltaRotation;
        rb.MoveRotation(targetRotation);
    }

}
