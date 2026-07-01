using System;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private ForcePushBack forcePushBack;
    private PlayerMovement playerMovement;

    void Start()
    {
        forcePushBack = player.GetComponent<ForcePushBack>();
        playerMovement = player.GetComponent<PlayerMovement>();

        forcePushBack.OnHitRotator += ReSpawnLogic;
    }

    void ReSpawnLogic(object sender,EventArgs eventArgs)
    {
        playerMovement.playerControlled = false;
    }
}