using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }
}
