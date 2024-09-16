using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet1 : MonoBehaviour
{
    [Header("General Bullet Stats")]
    [SerializeField] private LayerMask whatDestroyBullet;
    [SerializeField] private float destroyTime = 3f;

    [Header("Normal Bullet Stats")]
    [SerializeField] private float bulletVelocity = 5f;
    public float normalBulletDamage = 1.0f;

    


    private Rigidbody2D rb;
    //private float damage;
    private ObjectPool<Bullet1> _pool;
    private Coroutine deactivateBulletAfterTimeCoroutine;
    private bool isReleased = false;




    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //SetDestroyTime();


    }

    private void OnEnable()
    {
        deactivateBulletAfterTimeCoroutine = StartCoroutine(DeactivateBulletAfterTime());

        BulletVelocity();

        isReleased = false;


    }

    private void BulletVelocity()
    {
        rb.velocity = transform.up * bulletVelocity;
    }

    //private void SetDestroyTime()
    //{
    //    Destroy(gameObject, destroyTime);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReleased) return; // If already released, do nothing


        if ((whatDestroyBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
            if (iDamagable != null)
            {
                iDamagable.Damage(normalBulletDamage);
            }

            //Destroy(gameObject);
            ReleaseBullet();
        }
    }

    public void SetPool(ObjectPool<Bullet1> pool)
    {
        _pool = pool;
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
        _pool.Release(this); // Release the bullet to the pool
    }
}
