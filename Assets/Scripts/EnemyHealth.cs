using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float invincibility_Time { get; set; } = 1f;
    private float health;
    private bool is_Sheilded;
    private bool isInvincible;
    public event EventHandler EnemyHealthChange;
    private Coroutine activeShieldCoroutine;
    void Start()
    {
        health = 100.0f;
    }

    public bool TakeDamage(float damageAmount)
    {
        if (is_Sheilded) return false;
        if (isInvincible) return false;

        Debug.Log("Enemy health: " + health);
        health -= damageAmount;
        EnemyHealthChange?.Invoke(this, EventArgs.Empty);

        if (health <= 0)
        {
            health = 0;
            // on enemy death sequence 
        }
        else if (invincibility_Time > 0)
        {
            StartCoroutine(InvincibilityCoroutine());
        }

        return true;
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibility_Time);
        isInvincible = false;
    }

    public void Shield(float shield_Duration)
    {
        if (activeShieldCoroutine != null)
        {
            Debug.Log("EnemyShield already up");
            return;
        }
        activeShieldCoroutine = StartCoroutine(ShieldCoroutine(shield_Duration));
    }

    private IEnumerator ShieldCoroutine(float shield_Duration)
    {
        is_Sheilded = true;
        yield return new WaitForSeconds(shield_Duration);
        is_Sheilded = false;
        activeShieldCoroutine = null;
    }
}
