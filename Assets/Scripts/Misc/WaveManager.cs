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
        public float spawnRate; 
        public int minAsteroids; 
        public int maxAsteroids; 
        public float maxHealth;
        public float waveDuration; 
    }

    public Wave[] waves; 
    public AsteroidManager asteroidManager; 
    public Enemy _enemy;
    public float breakDuration = 10f; 
    public int currentWaveIndex = 0;
    public GameObject powerUp1;
    public GameObject powerUp2;
    public GameObject powerUp3;
    public Transform powerUp1Place;
    public Transform powerUp2Place;
    public Transform powerUp3Place;
    public List<GameObject> powerUpsList = new List<GameObject>(); 
    public TMP_Text waveText;
    public string sceneName;
    public TMP_Text preCountdownText;
    public MainMenuPauseUI mainMenuPauseUI;


    private void Start()
    {
        
        if (currentWaveIndex == 0)
        {
            StartCoroutine(StartWaveRoutine());
        }

        
    }

    private void Update()
    {
        waveText.text = "Wave: " + (currentWaveIndex + 1).ToString(); 
    }

    
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
        
        Time.timeScale = 0f;

        
        preCountdownText.gameObject.SetActive(true);

        
        preCountdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        preCountdownText.text = "Go!!";
        yield return new WaitForSecondsRealtime(1f);

        
        preCountdownText.gameObject.SetActive(false);

        
        Time.timeScale = 1f;

    }

    
    public IEnumerator StartWaveRoutine()
    {
        while (currentWaveIndex < waves.Length)
        {
            
            //yield return CountdownBeforeWave();
            yield return StartCoroutine(StartCountdown());


            
            //Wave currentWave = waves[currentWaveIndex];

            
            asteroidManager.spawnRate = waves[currentWaveIndex].spawnRate;
            asteroidManager.minAsteroids = waves[currentWaveIndex].minAsteroids;
            asteroidManager.maxAsteroids = waves[currentWaveIndex].maxAsteroids;
            _enemy.maxHealth = waves[currentWaveIndex].maxHealth;

            
            asteroidManager.InvokeRepeating("SpawnAsteroids", 0f, asteroidManager.spawnRate);
            yield return new WaitForSeconds(waves[currentWaveIndex].waveDuration);

            
            asteroidManager.CancelInvoke("SpawnAsteroids");

            
            yield return PlayerUpgradePhase();

            
            currentWaveIndex++;
        }

       
        Debug.Log("All waves completed!");

        

        SceneManager.LoadScene(sceneName); 
    }


    
    private IEnumerator PlayerUpgradePhase()
    {
        Debug.Log("Choose an upgrade!"); 
        GameObject p1 = Instantiate(powerUp1, powerUp1Place.position, Quaternion.identity);
        GameObject p2 = Instantiate(powerUp2, powerUp2Place.position, Quaternion.identity);
        GameObject p3 = Instantiate(powerUp3, powerUp3Place.position, Quaternion.identity);

        
        powerUpsList.Add(p1);
        powerUpsList.Add(p2);
        powerUpsList.Add(p3);

        
        yield return new WaitUntil(() => powerUpsList.Count < 3);

        
        foreach (GameObject powerUp in powerUpsList)
        {
            Destroy(powerUp); 
        }

        powerUpsList.Clear(); 
        Debug.Log("Upgrades completed, next wave!");
    }

    public void ContinueFromLoadedWave()
    {
        ResetWave(); 

        
        StartCoroutine(StartWaveRoutine());
    }

    
    public void ResetWave()
    {
        
        asteroidManager.CancelInvoke("SpawnAsteroids");

        
        asteroidManager.ClearAsteroids(); 

        
        StopAllCoroutines();
    }

    

    
    public void ClearAsteroids()
    {
        GameObject[] remainingAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in remainingAsteroids)
        {
            Destroy(asteroid);
        }
    }
}


