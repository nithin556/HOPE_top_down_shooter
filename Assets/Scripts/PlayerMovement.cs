using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera main_camera;
    [SerializeField] private float moveSpeed;
    private Vector3 movedir;

    void Update()
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
        transform.position += movedir * moveSpeed * Time.deltaTime;

    }
}
