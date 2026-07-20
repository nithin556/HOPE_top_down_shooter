using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Aim : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject gun;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask mask;
    [SerializeField] public Bullet bulletPrefab;
    [SerializeField] public Transform tip;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletForce;
    private Rigidbody rb;
    Vector3 hitPoint = Vector3.zero;
    private float nextFireTime;
    private IObjectPool<Bullet> objectPool;
    public void SetPool(IObjectPool<Bullet> _pool)
    {
        objectPool = _pool;
    }

    void Update()
    {
        MouseToWorldPosition();
        Firing_Method();
    }

    private void Firing_Method()
    {
        bool isFiring = gameInput.GetIsFiring();
        if (isFiring && Time.time >= nextFireTime)
        {
            Bullet bullet = objectPool.Get();
            bullet.transform.position = tip.position;
            bullet.transform.rotation = tip.rotation;
            rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce((hitPoint - bullet.transform.position).normalized * bulletForce, ForceMode.Impulse);
            nextFireTime = Time.time + fireRate;
        }
    }

    void MouseToWorldPosition()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
        {
            hitPoint = hit.point;
            Vector3 lookDirection = hitPoint - gun.transform.position;


            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    void LateUpdate()
    {
        gun.transform.position = transform.position;
    }
}