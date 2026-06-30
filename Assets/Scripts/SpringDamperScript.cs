using UnityEngine;

public class SpringDamperScript : MonoBehaviour
{
    [SerializeField] private float hoverHeight;
    [SerializeField] private float maxClearHeight;
    [SerializeField] private float minClearHeight;
    [SerializeField] private float maxDist;
    [SerializeField] private float springStrength;
    [SerializeField] private float dampingStrength;
    [SerializeField] private float gravity;

    private float springAcc;
    private float totalAcc;
    public float compression {get; private set; }
    public float velY { get; private set; }
    private Vector3 posY;

    void Start()
    {
        posY = transform.position;
    }

    private void Update()
    {
        SpringDamp();
    }

    private void SpringDamp()
    {
        totalAcc = 0f;
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

        posY = transform.position;// cache currentpos
        velY += totalAcc * Time.deltaTime;
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
    }
    public Vector3 GetSpringDampPos()
    {
        return posY;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.darkRed;
        Gizmos.DrawRay(transform.position, Vector3.down * maxDist);
        Gizmos.color = Color.darkGreen;
        Gizmos.DrawRay(transform.position, Vector3.down * hoverHeight);
    }


}
