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
    private bool isReleased = false; 


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
        rb.velocity = transform.up * bullet2Velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroyBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            if (isReleased) return; 

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

        if (!isReleased) 
        {
            ReleaseBullet();
        }
    }

    private void ReleaseBullet()
    {
        isReleased = true; 
        if (deactivateBulletAfterTimeCoroutine != null)
        {
            StopCoroutine(deactivateBulletAfterTimeCoroutine); 
        }
        _pool2.Release(this); 
    }
}
