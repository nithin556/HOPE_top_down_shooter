using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera main_camera;
    [SerializeField] private float moveSpeed;
    private float initialMoveSpeed;
    private float inititalRotationSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float MaxDistanceSphereCast;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float moveReduceTime = 2.0f;
    private SpringDamperScript springDamperScript;
    private PlayerRespawnerScript playerRespawnerScript;
    private Vector3 movedir;
    private float currentSpeedMultiplier = 1f;
    public bool moveHitReduce { get; private set; }
    public bool playerControlled;
    float deltaTimerCount;
    private Coroutine ActiveSlowRoutine;
    private void Start()
    {
        springDamperScript = GetComponent<SpringDamperScript>();
        moveHitReduce = false;
        initialMoveSpeed = moveSpeed;
        inititalRotationSpeed = rotateSpeed;
    }

    void Update()
    {
        if (playerControlled)
        {
            Movement();
        }
    }

    public void MoveReduction(float slow_Movement_Multiplier, float slow_Movement_Duration)
    {
        if (ActiveSlowRoutine != null)
        {
            StopCoroutine(ActiveSlowRoutine);
        }
        ActiveSlowRoutine = StartCoroutine(SpeedModifierRoutine(slow_Movement_Multiplier, slow_Movement_Duration));
    }

    public IEnumerator SpeedModifierRoutine(float slow_Movement_Multiplier, float slow_Movement_Duration)
    {
        currentSpeedMultiplier = slow_Movement_Multiplier;
        yield return new WaitForSeconds(slow_Movement_Duration);

        currentSpeedMultiplier = 1f;
        ActiveSlowRoutine = null;
    }
    private void Movement()
    {
        float finalNewSpeed = moveSpeed * currentSpeedMultiplier;
        float finalRotateSpeed = rotateSpeed * currentSpeedMultiplier;
        BasicMovement();
        if (springDamperScript.enabled == true)
        {
            transform.position = springDamperScript.GetSpringDampPos() + (movedir * finalNewSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += movedir * finalNewSpeed * Time.deltaTime;
        }
        SmoothRotate(finalRotateSpeed);
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
        if (Physics.SphereCast(transform.position, 0.5f, movedir, out RaycastHit hit, MaxDistanceSphereCast, wallLayerMask))
        {
            movedir = Vector3.ProjectOnPlane(movedir, hit.normal).normalized;
        }
    }

    private void SmoothRotate(float finalRotateSpeed)
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
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * finalRotateSpeed);
    }
}