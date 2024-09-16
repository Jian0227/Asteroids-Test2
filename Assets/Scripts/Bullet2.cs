using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet2 : MonoBehaviour
{
    [Header("General Bullet Stats")]
    [SerializeField] private LayerMask whatDestroyBullet;
    [SerializeField] private float destroyTime = 3f;

    [Header("Bullet2 Stats")]
    public float bullet2Velocity = 5f;
    public float bullet2Damage = 1.0f;

    private Rigidbody2D rb;
    private Coroutine deactivateBulletAfterTimeCoroutine;
    private ObjectPool<Bullet2> _pool2;
    private bool isReleased = false; // To track if the bullet has already been released


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //SetDestroyTime();


    }

    private void OnEnable()
    {
        deactivateBulletAfterTimeCoroutine = StartCoroutine(DeactivateBulletAfterTime());

        BulletVelocity();

        isReleased = false; // Reset flag when bullet is enabled

    }

    private void BulletVelocity()
    {
        rb.velocity = transform.up * bullet2Velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroyBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            if (isReleased) return; // If already released, do nothing

            IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
            if (iDamagable != null)
            {
                iDamagable.Damage(bullet2Damage);
            }

            //Destroy(gameObject);
            ReleaseBullet();
        }
    }

    public void SetPool(ObjectPool<Bullet2> pool2)
    {
        _pool2 = pool2;
    }

    private IEnumerator DeactivateBulletAfterTime()
    {
        yield return new WaitForSeconds(destroyTime);

        if (!isReleased) // Only release if not already released
        {
            ReleaseBullet();
        }
    }

    private void ReleaseBullet()
    {
        isReleased = true; // Set the flag to true to prevent multiple releases
        if (deactivateBulletAfterTimeCoroutine != null)
        {
            StopCoroutine(deactivateBulletAfterTimeCoroutine); // Stop the coroutine
        }
        _pool2.Release(this); // Release the bullet to the pool
    }
}
