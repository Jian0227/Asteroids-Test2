using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private float[] bullet1Levels = new float[6] { 0.5f, 0.4f, 0.3f, 0.2f, 0.1f, 0.05f };
    public int currentBullet1PowerUpLevel = 0;

    [SerializeField] private int[] bullet2Levels = new int[6] { 3, 4, 5, 6, 7, 8 };
    public int currentBullet2PowerUpLevel = 0;

    [SerializeField] private int[] bullet3Levels = new int[6] { 3, 4, 5, 6, 7, 8 };
    public int currentBullet3PowerUpLevel = 0;

    PlayerShoot _playerShoot;

    public TMP_Text powerup1Text;
    public TMP_Text powerup2Text;
    public TMP_Text powerup3Text;

    public static PowerUpManager instance;


    private void Start()
    {
        _playerShoot = FindObjectOfType<PlayerShoot>();
        UpdateBullet1FireRate();
        UpdateBullet2Amount();
        UpdateBullet3Amount();
    }

    // Update is called once per frame
    void Update()
    {
        powerup1Text.text = "Red Turret: Lv. " + currentBullet1PowerUpLevel.ToString();
        powerup2Text.text = "Blue Turret: Lv. " + currentBullet2PowerUpLevel.ToString();
        powerup3Text.text = "Green Turret: Lv. " + currentBullet3PowerUpLevel.ToString();

        UpdateBullet1FireRate();
        UpdateBullet2Amount();
        UpdateBullet3Amount();  

    }

    private void UpdateBullet1FireRate()
    {
        _playerShoot.timeBetweenShooting = bullet1Levels[currentBullet1PowerUpLevel];
    }

    public void CollectPowerUp1()
    {
        if (currentBullet1PowerUpLevel < bullet1Levels.Length - 1)
        {
            currentBullet1PowerUpLevel++;
            UpdateBullet1FireRate();
        }
        else
        {
            Debug.Log("Max power-up level reached.");
        }
    }

    private void UpdateBullet2Amount()
    {
        _playerShoot.bullet2Amount = bullet2Levels[currentBullet2PowerUpLevel];
    }

    public void CollectPowerUp2()
    {
        if (currentBullet2PowerUpLevel < bullet2Levels.Length - 1)
        {
            currentBullet2PowerUpLevel++;
            UpdateBullet2Amount();
        }
        else
        {
            Debug.Log("Max power-up level reached.");
        }
    }

    private void UpdateBullet3Amount()
    {
        _playerShoot.bullet3Amount = bullet3Levels[currentBullet3PowerUpLevel];
    }

    public void CollectPowerUp3()
    {
        if (currentBullet3PowerUpLevel < bullet3Levels.Length - 1)
        {
            currentBullet3PowerUpLevel++;
            UpdateBullet3Amount();
        }
        else
        {
            Debug.Log("Max power-up level reached.");
        }
    }
}
