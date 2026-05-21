//using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera main_camera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float hoverHeight;
    [SerializeField] private float maxClearHeight;
    [SerializeField] private float minClearHeight;
    [SerializeField] private float maxDist;
    [SerializeField] private float springStrength;
    [SerializeField] private float dampingStrength;
    [SerializeField] private float gravity;
    private float springAcc;
    private float compression;
    private Vector3 movedir;
    
    private float velY;

    [SerializeField] private float MaxDistanceRaycastForward;

    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        BasicMovement();
        SpringDamper();
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

        //if something in front of the player, stop movement
        if(Physics.Raycast(transform.position, movedir, out RaycastHit hit, MaxDistanceRaycastForward))
        {
            //try along movedir x axis
            Vector3 movedirX = new Vector3(movedir.x, 0, 0);
            Vector3 movedirZ = new Vector3(0, 0, movedir.z);
            movedir = Vector3.zero;

            //if u dont hit anything alongx
            if(!Physics.Raycast(transform.position, movedirX, out RaycastHit hitX, MaxDistanceRaycastForward))
            {
                movedir = movedirX;
            }

            //if u hit along x then try y
            else
            {
                //if u dont hit anything in y axis
                if(!Physics.Raycast(transform.position, movedirZ, out RaycastHit hitX2, MaxDistanceRaycastForward))
                {
                    movedir = movedirZ;
                    
                }
            }
        }

       
        

    }

    private void SpringDamper()
    {
        float totalAcc = 0f;
        totalAcc += gravity;

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, maxDist);

        if (isGrounded)
        {
            compression = hoverHeight - hit.distance;
            if (compression > 0f)
            {
                springAcc = (springStrength * compression) - (velY * dampingStrength);
                totalAcc += springAcc;
            }
        }

        velY += totalAcc * Time.deltaTime;
        Vector3 posY = transform.position;
        posY.y += velY * Time.deltaTime;

        if (isGrounded)
        {
            float minY = hit.point.y + minClearHeight;
            float maxY = hit.point.y + maxClearHeight;

            if (posY.y < minY)// if pos less than min height
            {
                if (velY < 0f)// if its coming down
                {
                    velY = 0;
                    posY.y = minY;
                }
            }
            if (posY.y > maxY) // if pos greater than max height
            {
                if (velY > 0f)// if it's going up
                {
                    velY = 0;
                    posY.y = maxY;
                }
            }
        }

        transform.position = posY + (movedir * moveSpeed * Time.deltaTime);
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

    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawRay(transform.position, Vector3.down * maxDist);
        Gizmos.color = Color.darkGreen;
        Gizmos.DrawRay(transform.position, Vector3.down * hoverHeight);
    }
}
