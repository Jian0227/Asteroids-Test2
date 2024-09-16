using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp3 : MonoBehaviour
{
    //public PowerUpEfffect powerUpEfffect;
    private PowerUpManager _manager;
    private WaveManager _waveManager;
    //[SerializeField] private int amount;

    private void Start()
    {
        _manager = FindObjectOfType<PowerUpManager>();
        _waveManager = FindObjectOfType<WaveManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _waveManager.powerUpsList.Remove(gameObject);

            Destroy(gameObject);

            _manager.CollectPowerUp3();
        }

    }
}
