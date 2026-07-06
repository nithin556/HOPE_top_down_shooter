using System;
using System.Collections;
using UnityEngine;

public class ForcePushBack : MonoBehaviour
{
    [SerializeField] private float holdAtSpawn;
    private PlayerMovement playerMovement;
    private SpringDamperScript springDamperScript;
    private BoxCollider playerBoxCollider;
    private PlayerHealth playerHealth;
    public event EventHandler OnDeath;
    private bool isDead = false;
    private Coroutine KnockBackstore;
    void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();
        springDamperScript = GetComponent<SpringDamperScript>();
        playerBoxCollider = GetComponent<BoxCollider>();
        playerHealth = GetComponent<PlayerHealth>();

    }
    void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.TryGetComponent<Hazard>(out var hazard))
        {
            float KnockBackDuration = hazard.Data.KnockBackDuration;
            Vector3 KnockBackPoint = hazard.GetKnockBackPoint();
            int damageAmount = hazard.Data.damageAmount;
            if(playerHealth.health != 0 && damageAmount<playerHealth.health)
            {
                StartCoroutine(KnockBack(KnockBackPoint,KnockBackDuration,damageAmount,false));
            }
            else
            {
                OnDeath?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void GetLevelRespawn(Vector3 levelRespawn,float KnockBackDuration,bool healthReset)
    {
        StopAllCoroutines(); // Kills the active knockback loop cleanly
        StartCoroutine(KnockBack(levelRespawn,KnockBackDuration,0, healthReset));
    }

    IEnumerator KnockBack(Vector3 KnockBackPoint,float KnockBackDuration,int damageAmount,bool healthReset)
    {
        playerBoxCollider.enabled = false;
        playerMovement.playerControlled = false;
        playerHealth.TakeDamage(damageAmount);
        Debug.Log(playerHealth.health);
        
        Vector3 hitPos = transform.position;
        float i = 0;

        while (i < KnockBackDuration)
        {
            transform.position = Vector3.Lerp(hitPos,KnockBackPoint,i/KnockBackDuration);
            i += Time.deltaTime;
            yield return null;
        }
        if (healthReset)
        {
            playerHealth.HealthReset();
        }
        yield return new WaitForSeconds(holdAtSpawn);
        springDamperScript.totalAcc = 0;
        springDamperScript.velY = 0;
        playerBoxCollider.enabled = true;
        playerMovement.playerControlled = true;

    }

}