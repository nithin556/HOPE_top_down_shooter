using System;
using System.Collections;
using UnityEngine;

public class ForcePushBack : MonoBehaviour
{
    public event EventHandler OnHitRotator;

    private Transform knockBackPoint;
    private float knockBackLerp_Seconds;
    [SerializeField] private float holdAtSpawn;
    private PlayerMovement playerMovement;
    private ForcePushBack forcePushBack;
    private SpringDamperScript springDamperScript;
    private BoxCollider playerBoxCollider;
    void Start()
    {
        forcePushBack = GetComponent<ForcePushBack>();
        forcePushBack.OnHitRotator += forcePushBackLogic;

        playerMovement = GetComponent<PlayerMovement>();
        springDamperScript = GetComponent<SpringDamperScript>();
        playerBoxCollider = GetComponent<BoxCollider>();

    }
    void OnCollisionEnter(Collision collision)
    {
        //for now works only for rotator must account for all
        Rotator rotatorCollison = collision.gameObject.GetComponent<Rotator>();
        if(rotatorCollison != null)
        {
            knockBackPoint = rotatorCollison.GetKnockBackPoint();
            knockBackLerp_Seconds = rotatorCollison.GetKnockBackLerp_Seconds();
            OnHitRotator?.Invoke(this,EventArgs.Empty);    
        }
        else
        {
            Debug.Log("Not RotatorObstacle !");
        }
    }

    void forcePushBackLogic(object sender,EventArgs eventArgs)
    {
        StartCoroutine(ReSpawn());
    }

    IEnumerator ReSpawn()
    {
        playerBoxCollider.enabled = false;
        playerMovement.playerControlled = false;
        Vector3 hitPos = transform.position;
        float i = 0;

        while (i < knockBackLerp_Seconds)
        {
            transform.position = Vector3.Lerp(hitPos,knockBackPoint.position,i/knockBackLerp_Seconds);
            i += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(holdAtSpawn);
        springDamperScript.totalAcc = 0;
        springDamperScript.velY = 0;
        playerBoxCollider.enabled = true;
        playerMovement.playerControlled = true;
    }

}
