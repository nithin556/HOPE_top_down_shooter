using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> objectPool;
    private Coroutine BulletTimerCoroutine;

    public void SetPool(IObjectPool<Bullet> _pool)
    {
        objectPool = _pool;
    }

    void OnEnable()
    {
        BulletTimerCoroutine = StartCoroutine(TimerCo(5f));
    }
    void OnDisable()
    {
        if (BulletTimerCoroutine != null)
        {
            StopCoroutine(BulletTimerCoroutine);
            BulletTimerCoroutine = null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet component))
        {
            return;
        }
        ReturnToPool();
    }

    private IEnumerator TimerCo(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        objectPool?.Release(this);
    }
}
