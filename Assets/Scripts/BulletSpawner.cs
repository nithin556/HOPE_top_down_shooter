using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    private ObjectPool<Bullet> _pool;
    private Aim aimScript;
    [SerializeField] private Transform bulletInstanceParent;

    void Awake()
    {
        aimScript = GetComponent<Aim>();
        _pool = new ObjectPool<Bullet>(
            createFunc: CreateFunction,
            actionOnGet: ActionOnGet,
            actionOnRelease: ActionOnRelease,
            actionOnDestroy: ActionOnDestroy,
            defaultCapacity: 50,
            maxSize: 100
        );
        aimScript.SetPool(_pool);
    }
    private Bullet CreateFunction()
    {
        Bullet bullet = Instantiate(aimScript.bulletPrefab, aimScript.tip.position, Quaternion.identity, bulletInstanceParent);
        bullet.SetPool(_pool);
        return bullet;
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}