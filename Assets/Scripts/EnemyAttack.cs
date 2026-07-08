using System;
using UnityEngine;

//just a temporary implmentation of attack
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform playerPos;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int damage = 1;
    private PlayerHealth playerHealth;

    private event EventHandler OnHit;

    Rigidbody swordRB;

    float countdown;
    [SerializeField] float attackTimer = 0f;


    void Start()
    {
        playerHealth = playerPos.GetComponent<PlayerHealth>();
        swordRB = weapon.GetComponentInChildren<Rigidbody>();
        Debug.Log(swordRB);
        countdown = -1;
    }
    void OnTriggerEnter(Collider other)
    {
        if (countdown < 0)
        {
            countdown = attackTimer;
            //OnHit?.Invoke(this, EventArgs.Empty);
            if (other.gameObject.TryGetComponent<PlayerHealth>(out var playerHealth))
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("hit player");
            }
            else
            {
                Debug.Log("not player");
            }
        }
    }

    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        Debug.Log(countdown);
    }

}
