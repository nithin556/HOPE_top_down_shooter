using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform knockBackPoint;
    [SerializeField] private float knockBackLerp_Seconds;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotationSpeed = rotationSpeed * Mathf.Deg2Rad;
    }

    void FixedUpdate()
    {
        rb.angularVelocity = Vector3.up * rotationSpeed;
    }

    public Transform GetKnockBackPoint()
    {
        return knockBackPoint;
    }
    public float GetKnockBackLerp_Seconds()
    {
        return knockBackLerp_Seconds;
    }


}
