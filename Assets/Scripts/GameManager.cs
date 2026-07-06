using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform levelRESpawnPoint;
    private PlayerHealth playerHealth;
    private ForcePushBack forcePushBack;

    void OnEnable()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        forcePushBack = player.GetComponent<ForcePushBack>();
        forcePushBack.OnDeath += levelRespawn;
        
    }
    void OnDisable()
    {
        forcePushBack.OnDeath -= levelRespawn;
    }
    void Start()
    {

    }

    private void levelRespawn(object sender,EventArgs eventArgs)
    {
        forcePushBack.GetLevelRespawn(levelRESpawnPoint.position,2f,true);
    }

}
