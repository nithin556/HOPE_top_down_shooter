using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [field: SerializeField] public Transform levelRespawn { get; private set; }
    private PlayerHealth playerHealth;
    float timer;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.HealthReset();

    }
    void Update()
    {

    }

}
