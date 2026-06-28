using UnityEngine;

public class EnemySpringMovementVertical : MonoBehaviour
{
    private SpringDamperScript springDamperScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        springDamperScript = GetComponent<SpringDamperScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Ypos = springDamperScript.GetSpringDampPos();;
        Vector3 currentpos = transform.position;
        currentpos.y = Ypos.y;
        transform.position = currentpos;
    }
}
