using UnityEngine;

public class EnemySpringMovementVertical : MonoBehaviour
{
    private SpringDamperScript springDamperScript;

    void Start()
    {
        springDamperScript = GetComponent<SpringDamperScript>();
    }


    void Update()
    {
        transform.position = springDamperScript.GetSpringDampPos();
    }
}
