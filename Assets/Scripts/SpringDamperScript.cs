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
    [SerializeField] private float scaleFactor;
    [SerializeField] private Transform enemyModel;
    private float springAcc;
    private float compression;

    private float velY;

    void Update()
    {
        SpringDamp();
    }

    private void SpringDamp()
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

        transform.position = posY;

        //Vector3 scaleY = enemyModel.transform.localScale;
        //scaleY.y = velY;
        //enemyModel.transform.localScale = scaleY * scaleFactor;
    }

}
