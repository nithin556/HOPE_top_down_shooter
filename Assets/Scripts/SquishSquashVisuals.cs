using UnityEngine;

public class SquishSquashVisuals : MonoBehaviour
{
    [SerializeField] private float scaleFactor;
    private Vector3 currentPos;
    private Vector3 prevPos;
    private Vector3 displacement;
    private Vector3 velocity;
    private Vector3 zeroVector ;
    void Start()
    {
        prevPos = transform.position;
    }

    void Update()
    {
        SquishSquash();
    }

    void SquishSquash()
    {
        displacement = transform.position - prevPos;
        velocity = displacement/Time.deltaTime;

        
    }
    void LateUpdate()
    {
        prevPos = transform.position;
    }
}
