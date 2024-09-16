using UnityEngine;
using UnityEngine.UI; 

public class MainMenuPauseUI : MonoBehaviour
{
    public bool gameIsPaused = true; 
    public GameObject pauseMenuUI; 
    public SaveManager saveManager;
    public WaveManager waveManager;
    public PlayerHealth playerHealth;
    public PowerUpManager powerUpManager;
    public Button loadGameButton; 


    //private void Awake()
    //{
    //    Pause();
    //}
    void Start()
    {
        
        if (!SaveSystem.DoesSaveFileExist())
        {
            loadGameButton.interactable = false; 
        }

        
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    
    public void StartGame()
    {
        Resume(); 

        
        //if (waveManager.currentWaveIndex == 0)
        //{
        //    waveManager.StartCoroutine(waveManager.StartWaveRoutine());
        //}
    }

    
    public void Resume()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        gameIsPaused = false;
    }

    
    public void Pause()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f; 
        gameIsPaused = true;
    }

    // Load the saved game state
    public void LoadGame()
    {
        if (SaveSystem.DoesSaveFileExist()) 
        {
            saveManager.LoadGame(); 
            Resume(); 
        }
        else
        {
            Debug.LogWarning("No saved game found!");
        }
    }

    
    public void SaveGame()
    {
        saveManager.SaveGame();
        Debug.Log("Game Saved!");
        loadGameButton.interactable = true; 
    }

    
    public void ResetGame()
    {
        
        waveManager.currentWaveIndex = 0; 
        playerHealth.health = 10; 
        powerUpManager.currentBullet1PowerUpLevel = 0; 
        powerUpManager.currentBullet2PowerUpLevel = 0;
        powerUpManager.currentBullet3PowerUpLevel = 0;

        waveManager.ResetWave();
        waveManager.ContinueFromLoadedWave();


        Resume(); 
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}


