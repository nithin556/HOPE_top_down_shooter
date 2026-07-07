using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform levelRESpawnPoint;
    private PlayerHealth playerHealth;
    private ForcePushBack forcePushBack;
    public event EventHandler<HealthEventDataPass> HealthChangeUI;

    void OnEnable()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        forcePushBack = player.GetComponent<ForcePushBack>();
        forcePushBack.OnDeath += levelRespawn;
        playerHealth.HealthChange += OnHealthChange;

    }
    void OnDisable()
    {
        forcePushBack.OnDeath -= levelRespawn;
    }
    void Start()
    {
        playerHealth.HealthReset();
    }

    private void levelRespawn(object sender, EventArgs eventArgs)
    {
        forcePushBack.GetLevelRespawn(levelRESpawnPoint.position, 2f, true);
    }

    private void OnHealthChange(object sender, EventArgs eventArgs)
    {
        //Debug.Log("Health : " + playerHealth.health);


        HealthChangeUI?.Invoke(this, new HealthEventDataPass
        {
            playerHealthGM = playerHealth.health
        });

    }

}