using UnityEngine;

public class SquishSquashVisuals : MonoBehaviour
{
    [SerializeField] private float scaleFactor;
    private SpringDamperScript springDamper;
    void Start()
    {
        springDamper = GetComponentInParent<SpringDamperScript>();
        
    }

    void Update()
    {
        SquishSquash();
    }

    void SquishSquash()
    {
        Vector3 scaleY = transform.localScale;
        scaleY.y = Mathf.Clamp(springDamper.totalAcc,1f,1f*scaleFactor);
        scaleY.x = 1 / scaleY.y;
        scaleY.z = 1 / scaleY.y;
        Vector3 smoothScale = Vector3.Lerp(transform.localScale, scaleY,Time.deltaTime);
        transform.localScale = smoothScale;
    }
}
