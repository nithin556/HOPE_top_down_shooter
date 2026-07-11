using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float health { get; private set; }
    private bool isInvincible;
    public float invincibility_Time { get; set; }
    private bool is_Sheilded;
    private float shield_Duration;
    private Coroutine shieldActiveCoroutine;
    public event EventHandler HealthChange;
    public event EventHandler OnDie;

    void Start()
    {
        health = 100;
    }

    public bool TakeDamage(float damageAmount)
    {
        if (is_Sheilded) return false;
        if (isInvincible) return false;

        health -= damageAmount;
        HealthChange?.Invoke(this, EventArgs.Empty);

        if (health <= 0)
        {
            health = 0;
            OnDie?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
        }

        return true;
    }
    public void Shield(float shield_Duration)
    {
        if (shieldActiveCoroutine != null)
        {
            Debug.Log("Shield already up");
            return;
        }
        shieldActiveCoroutine = StartCoroutine(ShieldRoutine(shield_Duration));
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibility_Time);
        isInvincible = false;
    }
    private IEnumerator ShieldRoutine(float shield_Duration)
    {
        is_Sheilded = true;
        yield return new WaitForSeconds(shield_Duration);
        is_Sheilded = false;
        shieldActiveCoroutine = null;
    }


    public void HealthReset()
    {
        health = 100;
        HealthChange?.Invoke(this, EventArgs.Empty);
    }

}
