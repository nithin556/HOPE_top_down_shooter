using UnityEngine;

//just a temporary implmentation of attack
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float swingSpeed;

    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * swingSpeed);
    }
}