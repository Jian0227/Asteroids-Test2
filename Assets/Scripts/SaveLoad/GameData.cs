using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int wave;
    public int playerHealth;
    public int powerUp1;
    public int powerUp2;
    public int powerUp3;

    public GameData(WaveManager waveManager, PlayerHealth _playerHealth, PowerUpManager powerUpManager)
    {
        wave = waveManager.currentWaveIndex;
        playerHealth = _playerHealth.health;
        powerUp1 = powerUpManager.currentBullet1PowerUpLevel;
        powerUp2 = powerUpManager.currentBullet2PowerUpLevel;
        powerUp3 = powerUpManager.currentBullet3PowerUpLevel;
    }
}

