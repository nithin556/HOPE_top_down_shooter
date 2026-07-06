using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health {get; private set;}

    void Start()
    {
        health = 100;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health < 0)
        {
            health = 0;
        }
    
    }
    public void HealthReset()
    {
        health = 100;
    }
}
