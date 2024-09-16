using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float maxHealth;

    [SerializeField] private float currentHealth;
    private AsteroidManager _asteroidManager;




    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        _asteroidManager = FindObjectOfType<AsteroidManager>();

    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
            _asteroidManager.ReturnAsteroidToPool(gameObject);

        }
    }
}
