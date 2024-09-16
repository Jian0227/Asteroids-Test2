using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet3 : MonoBehaviour
{
    [Header("General Bullet Stats")]
    [SerializeField] private LayerMask whatDestroyBullet;
    [SerializeField] private float destroyTime = 3f;

    [Header("Bullet3 Stats")]
    public float bullet3Velocity = 5f;
    public float bullet3Damage = 1.0f;

    private Rigidbody2D rb;
    private Coroutine deactivateBulletAfterTimeCoroutine;
    private ObjectPool<Bullet3> _pool3;
    private bool isReleased = false; // To track if the bullet has already been released

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isReleased = false; // Reset flag when bullet is enabled
        deactivateBulletAfterTimeCoroutine = StartCoroutine(DeactivateBulletAfterTime());
        BulletVelocity();
    }

    private void BulletVelocity()
    {
        rb.velocity = transform.up * bullet3Velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReleased) return; // If already released, do nothing

        if ((whatDestroyBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
            if (iDamagable != null)
            {
                iDamagable.Damage(bullet3Damage);
            }

            ReleaseBullet();
        }
    }

    public void SetPool(ObjectPool<Bullet3> pool3)
    {
        _pool3 = pool3;
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
        _pool3.Release(this); // Release the bullet to the pool
    }
}

