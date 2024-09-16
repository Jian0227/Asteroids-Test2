using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public float spawnRate; // How fast asteroids spawn in this wave
        public int minAsteroids; // Minimum number of asteroids spawned in one go
        public int maxAsteroids; // Maximum number of asteroids spawned in one go
        public float maxHealth;
        public float waveDuration; // How long the wave lasts in seconds
    }

    public Wave[] waves; // Array of waves to be configured in the Inspector
    public AsteroidManager asteroidManager; // Reference to the AsteroidManager script
    public Enemy _enemy;
    public float breakDuration = 10f; // Time between waves for player to choose upgrades
    public int currentWaveIndex = 0;
    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;
    public Transform powerUp1Place;
    public Transform powerUp2Place;
    public Transform powerUp3Place;
    public List<GameObject> powerUpsList = new List<GameObject>(); // List to store collected power-ups
    public TMP_Text waveText;
    public string sceneName;
    public TMP_Text preCountdownText;
    public MainMenuPauseUI mainMenuPauseUI;


    private void Start()
    {
        // Start the first wave or continue from a loaded wave
        if (currentWaveIndex == 0)
        {
            StartCoroutine(StartWaveRoutine());
        }

        //if (!MainMenuPauseUI.gameIsPaused)
        //{
        //    StartCoroutine(StartWaveRoutine());
        //}
    }

    private void Update()
    {
        waveText.text = "Wave: " + (currentWaveIndex + 1).ToString(); // Display wave number
    }

    // Countdown before each wave starts
    //private IEnumerator CountdownBeforeWave()
    //{
    //    for (int i = 3; i > 0; i--)
    //    {
    //        Debug.Log(i); // Replace with actual UI for countdown display (e.g., showing 3, 2, 1)
    //        yield return new WaitForSeconds(1);
    //    }
    //    Debug.Log("Start!"); // Replace with actual UI for "Start!" message
    //}

    private IEnumerator StartCountdown()
    {
        // Pause the game
        Time.timeScale = 0f;

        // Show pre-countdown text and hide the countdown timer text
        preCountdownText.gameObject.SetActive(true);

        // Countdown from 3 to 1
        preCountdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "Go!!";
        yield return new WaitForSecondsRealtime(1f);

        // Hide the pre-countdown text and show the countdown timer text
        preCountdownText.gameObject.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;

    }

    // Manage the flow of the waves
    public IEnumerator StartWaveRoutine()
    {
        while (currentWaveIndex < waves.Length)
        {
            // Show countdown before the wave starts
            //yield return CountdownBeforeWave();
            yield return StartCoroutine(StartCountdown());


            // Get the current wave parameters
            //Wave currentWave = waves[currentWaveIndex];

            // Set asteroid manager parameters
            asteroidManager.spawnRate = waves[currentWaveIndex].spawnRate;
            asteroidManager.minAsteroids = waves[currentWaveIndex].minAsteroids;
            asteroidManager.maxAsteroids = waves[currentWaveIndex].maxAsteroids;
            _enemy.maxHealth = waves[currentWaveIndex].maxHealth;

            // Start spawning asteroids for the duration of the wave
            asteroidManager.InvokeRepeating("SpawnAsteroids", 0f, asteroidManager.spawnRate);
            yield return new WaitForSeconds(waves[currentWaveIndex].waveDuration);

            // Stop spawning after the wave duration ends
            asteroidManager.CancelInvoke("SpawnAsteroids");

            // Wait for the player to choose upgrades
            yield return PlayerUpgradePhase();

            // Proceed to the next wave
            currentWaveIndex++;
        }

        // All waves completed, load the next scene
        Debug.Log("All waves completed!");

        // Optionally, perform any game-over logic or show final results here

        SceneManager.LoadScene(sceneName); // Load the new scene
    }


    // Handle the upgrade phase
    private IEnumerator PlayerUpgradePhase()
    {
        Debug.Log("Choose an upgrade!"); // Replace with actual UI for upgrade choices
        GameObject p1 = Instantiate(powerUp1, powerUp1Place.position, Quaternion.identity);
        GameObject p2 = Instantiate(powerUp2, powerUp2Place.position, Quaternion.identity);
        GameObject p3 = Instantiate(powerUp3, powerUp3Place.position, Quaternion.identity);

        // Add the power-ups to the list for tracking
        powerUpsList.Add(p1);
        powerUpsList.Add(p2);
        powerUpsList.Add(p3);

        // Wait for the player to collect one power-up (or another condition)
        yield return new WaitUntil(() => powerUpsList.Count < 3);

        // Destroy remaining power-ups
        foreach (GameObject powerUp in powerUpsList)
        {
            Destroy(powerUp); // Destroy each GameObject
        }

        powerUpsList.Clear(); // Clear the list after destruction
        Debug.Log("Upgrades completed, next wave!");
    }

    public void ContinueFromLoadedWave()
    {
        ResetWave(); // Reset before continuing to ensure nothing from the old wave continues

        // Start the loaded wave from the correct index
        StartCoroutine(StartWaveRoutine());
    }

    // Method to stop current wave processes
    public void ResetWave()
    {
        // Stop spawning asteroids or any ongoing processes
        asteroidManager.CancelInvoke("SpawnAsteroids");

        // Optionally clear remaining asteroids or enemies from the screen
        asteroidManager.ClearAsteroids(); // Assuming you have a method to clear them

        // Stop any ongoing coroutines related to waves
        StopAllCoroutines();
    }

    

    // Optionally add a method to clear all remaining asteroids
    public void ClearAsteroids()
    {
        GameObject[] remainingAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in remainingAsteroids)
        {
            Destroy(asteroid);
        }
    }
}


