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
            // Reset the current game state before loading
            waveManager.ResetWave();

            // Load saved data into the game
            waveManager.currentWaveIndex = data.wave;
            _playerHealth.health = data.playerHealth;
            powerUpManager.currentBullet1PowerUpLevel = data.powerUp1;
            powerUpManager.currentBullet2PowerUpLevel = data.powerUp2;
            powerUpManager.currentBullet3PowerUpLevel = data.powerUp3;

            // Continue from the saved wave
            waveManager.ContinueFromLoadedWave();
        }
        else
        {
            Debug.LogWarning("No saved game found!");
        }
    }
}





