using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Asteroid : MonoBehaviour
{
    [Header("General Asteroid Stats")]
    [SerializeField] private LayerMask whatDestroyAsteroid;
    [SerializeField] private float destroyTime = 3f;

    [Header("Asteroid Stats")]
    //public float asteroidVelocity = 5f;
    public float asteroidDamage = 1.0f;

    private Rigidbody2D rb;
    private Coroutine deactivateAsteroidAfterTimeCoroutine;
    private bool isReleased = false; 
    private AsteroidManager _asteroidManager;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isReleased = false; // Reset flag when bullet is enabled
        deactivateAsteroidAfterTimeCoroutine = StartCoroutine(DeactivateAsteroidAfterTime());
        _asteroidManager = FindObjectOfType<AsteroidManager>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        //AsteroidVelocity();
    }

    //private void AsteroidVelocity()
    //{
    //    rb.velocity = transform.up * asteroidVelocity;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReleased) return; // If already released, do nothing

        if (collision.gameObject.layer == 7)
        {
            //IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
            //if (iDamagable != null)
            //{
            //    iDamagable.Damage(asteroidDamage);
            //}
            _playerHealth.minusHealth();
            ReleaseAsteroid();
        }
        else if (collision.gameObject.layer == 8)
        {
            ReleaseAsteroid();
        }
    }

    //public void SetPool(ObjectPool<Bullet3> pool3)
    //{
    //    _pool3 = pool3;
    //}

    private IEnumerator DeactivateAsteroidAfterTime()
    {
        yield return new WaitForSeconds(destroyTime);

        if (!isReleased) // Only release if not already released
        {
            ReleaseAsteroid();
        }
    }

    private void ReleaseAsteroid()
    {
        isReleased = true; // Set the flag to true to prevent multiple releases
        if (deactivateAsteroidAfterTimeCoroutine != null)
        {
            StopCoroutine(deactivateAsteroidAfterTimeCoroutine); // Stop the coroutine
        }
        _asteroidManager.ReturnAsteroidToPool(gameObject);
    }
}
