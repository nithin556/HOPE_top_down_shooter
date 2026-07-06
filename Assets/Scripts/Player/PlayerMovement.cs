using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera main_camera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float MaxDistanceSphereCast;
    [SerializeField] private LayerMask wallLayerMask;
    private SpringDamperScript springDamperScript;
    private Vector3 movedir;
    public bool playerControlled;
    private void Start()
    {
        springDamperScript = GetComponent<SpringDamperScript>();
    }

    void Update()
    {
        if (playerControlled)
        {
            Movement();
        }
    }

    private void Movement()
    {
        BasicMovement();
        if(springDamperScript.enabled == true)
        {
            transform.position = springDamperScript.GetSpringDampPos() + (movedir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += movedir * moveSpeed * Time.deltaTime;
        }
        SmoothRotate();
    }
    private void BasicMovement()
    {
        Vector2 rawInputVector = gameInput.GetMovementVector();
        Vector3 refactoredInput = new Vector3(rawInputVector.x, 0, rawInputVector.y);

        Vector3 cam_forward = main_camera.transform.forward;
        Vector3 cam_right = main_camera.transform.right;
        cam_forward.y = 0;
        cam_right.y = 0;
        cam_forward.Normalize();
        cam_right.Normalize();
        movedir = refactoredInput.z * cam_forward + refactoredInput.x * cam_right;

        //slide along wall
        if(Physics.SphereCast(transform.position,0.5f,movedir, out RaycastHit hit, MaxDistanceSphereCast, wallLayerMask))
        {
            movedir = Vector3.ProjectOnPlane(movedir,hit.normal).normalized;
        }
    }

    private void SmoothRotate()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation;
        if (movedir != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(movedir, Vector3.up);
        }
        else
        {
            targetRotation = transform.rotation;
        }
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}