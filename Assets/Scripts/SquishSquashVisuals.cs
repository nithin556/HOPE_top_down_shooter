using Unity.Mathematics;
using UnityEngine;

public class SquishSquashVisuals : MonoBehaviour
{
    [SerializeField] private float scaleFactor;
    [SerializeField] private float compressionMult;
    private SpringDamperScript springDamperScript;
    private Vector3 baseScale;
    void Start()
    {
        springDamperScript = GetComponentInParent<SpringDamperScript>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        SquishSquash();
    }

    void SquishSquash()
    {
        
        float squishCompression = springDamperScript.compression;
        Vector3 customScale = baseScale;

        //if compression is zero then stretch
        if(squishCompression < 0f)
        {
            float result = Mathf.Abs(springDamperScript.velY);
            result = Mathf.Clamp(result,1f,scaleFactor);
            customScale.y = result;
            customScale.x = customScale.x/Mathf.Sqrt(result);
            customScale.z = customScale.z/Mathf.Sqrt(result);
            
            squishCompression = 0f;
        }
        //if positive then squish using compress variable
        else
        {
            float customCompression = Mathf.Clamp(squishCompression * compressionMult,1f,scaleFactor);
            customScale.x = customCompression;
            customScale.z = customCompression;
            customScale.y = customScale.y/Mathf.Sqrt(customCompression);
        }

        transform.localScale = customScale;
    }
}
