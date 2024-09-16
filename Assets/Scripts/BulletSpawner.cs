using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public ObjectPool<Bullet1> _pool;

    public ObjectPool<Bullet2> _pool2;

    public ObjectPool<Bullet3> _pool3;

    private PlayerShoot playerShoot;

    private void Start()
    {
        playerShoot = GetComponent<PlayerShoot>();
        _pool = new ObjectPool<Bullet1>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 500, 1000);
        _pool2 = new ObjectPool<Bullet2>(CreateBullet2, OnTakeBulletFromPool2, OnReturnBulletToPool2, OnDestroyBullet2, true, 500, 1000);
        _pool3 = new ObjectPool<Bullet3>(CreateBullet3, OnTakeBulletFromPool3, OnReturnBulletToPool3, OnDestroyBullet3, true, 500, 1000);

    }

    private Bullet1 CreateBullet()
    {
        Bullet1 bullet = Instantiate(playerShoot.bullet1, playerShoot.bulletSpawnPoint.position, playerShoot.bulletSpawnPoint.transform.rotation);

        bullet.SetPool(_pool);

        return bullet;
    }

    private void OnTakeBulletFromPool(Bullet1 bullet)
    {
        bullet.transform.position = playerShoot.bulletSpawnPoint.position;
        bullet.transform.up = playerShoot.bulletSpawnPoint.transform.up;

        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(Bullet1 bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet1 bullet)
    {
        Destroy(bullet.gameObject);
    }


    private Bullet2 CreateBullet2()
    {
        Bullet2 bullet2 = Instantiate(playerShoot.bullet2, playerShoot.bullet2SpawnPoint.position, playerShoot.bullet2SpawnPoint.transform.rotation);

        bullet2.SetPool(_pool2);

        return bullet2;
    }

    private void OnTakeBulletFromPool2(Bullet2 bullet2)
    {
        bullet2.transform.position = playerShoot.bullet2SpawnPoint.position;
        bullet2.transform.rotation = playerShoot.bullet2SpawnPoint.rotation;

        bullet2.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool2(Bullet2 bullet2)
    {
        bullet2.gameObject.SetActive(false);
    }

    private void OnDestroyBullet2(Bullet2 bullet2)
    {
        Destroy(bullet2.gameObject);
    }


    private Bullet3 CreateBullet3()
    {
        Bullet3 bullet3 = Instantiate(playerShoot.bullet3, playerShoot.bullet3SpawnPoint.position, playerShoot.bullet3SpawnPoint.transform.rotation);

        bullet3.SetPool(_pool3);

        return bullet3;
    }

    private void OnTakeBulletFromPool3(Bullet3 bullet3)
    {
        bullet3.transform.position = playerShoot.bullet3SpawnPoint.position;
        bullet3.transform.rotation = playerShoot.bullet3SpawnPoint.rotation;

        bullet3.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool3(Bullet3 bullet3)
    {
        bullet3.gameObject.SetActive(false);
    }

    private void OnDestroyBullet3(Bullet3 bullet3)
    {
        Destroy(bullet3.gameObject);
    }
}
