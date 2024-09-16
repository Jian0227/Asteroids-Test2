using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public WaveManager waveManager;
    public PlayerHealth _playerHealth;
    public PowerUpManager powerUpManager;

    public void SaveGame()
    {
        SaveSystem.SaveGame(waveManager, _playerHealth, powerUpManager);
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            
            waveManager.ResetWave();

            
            waveManager.currentWaveIndex = data.wave;
            _playerHealth.health = data.playerHealth;
            powerUpManager.currentBullet1PowerUpLevel = data.powerUp1;
            powerUpManager.currentBullet2PowerUpLevel = data.powerUp2;
            powerUpManager.currentBullet3PowerUpLevel = data.powerUp3;

            
            waveManager.ContinueFromLoadedWave();
        }
        else
        {
            Debug.LogWarning("No saved game found!");
        }
    }
}





