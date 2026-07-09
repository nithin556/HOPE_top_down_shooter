using System;
using System.Collections;
using UnityEngine;
public class PlayerRespawnerScript : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float holdAtSpawn = 0f;
    [SerializeField] private float respawnTravelDuration = 2.0f;
    private PlayerMovement playerMovement;
    private SpringDamperScript springDamperScript;
    private BoxCollider playerBoxCollider;
    private PlayerHealth playerHealth;
    public event EventHandler ReduceMovement;
    private Coroutine currentRespawnSequence;

    void Awake()
    {

        playerMovement = GetComponent<PlayerMovement>();
        springDamperScript = GetComponent<SpringDamperScript>();
        playerBoxCollider = GetComponent<BoxCollider>();
        playerHealth = GetComponent<PlayerHealth>();

    }
    void OnEnable()
    {
        playerHealth.OnDie += HandlePlayerDeath;
    }
    void OnDisable()
    {
        playerHealth.OnDie -= HandlePlayerDeath;
    }

    public void HandlePlayerDeath(object sender, EventArgs eventArgs)
    {
        Transform levelRespawnPoint = gameManager.levelRespawn;
        TriggerRespawnSequence(levelRespawnPoint.position, respawnTravelDuration);
    }
    private void TriggerRespawnSequence(Vector3 levelRespawnPoint, float duration)
    {
        if (currentRespawnSequence != null)//if its running stop and start again but since boxcoll diabled this wont happen
        {
            StopCoroutine(currentRespawnSequence);
        }
        currentRespawnSequence = StartCoroutine(LevelRespawnSequence(levelRespawnPoint, duration));

    }

    IEnumerator LevelRespawnSequence(Vector3 levelRespawnPoint, float duration)
    {
        playerBoxCollider.enabled = false;
        playerMovement.playerControlled = false;

        Vector3 startingPos = transform.position;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, levelRespawnPoint, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = levelRespawnPoint;
        playerHealth.HealthReset();

        yield return new WaitForSeconds(holdAtSpawn);

        springDamperScript.totalAcc = 0;
        springDamperScript.velY = 0;
        playerBoxCollider.enabled = true;
        playerMovement.playerControlled = true;

        currentRespawnSequence = null;

    }
}
