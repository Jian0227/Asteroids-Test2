using UnityEngine;
using UnityEngine.UI; // Needed for Button component

public class MainMenuPauseUI : MonoBehaviour
{
    public bool gameIsPaused = true; // Start the game paused
    public GameObject pauseMenuUI; // This will act as both the main menu and pause menu
    public SaveManager saveManager;
    public WaveManager waveManager;
    public PlayerHealth playerHealth;
    public PowerUpManager powerUpManager;
    public Button loadGameButton; // Reference to the Load Game button


    //private void Awake()
    //{
    //    Pause();
    //}
    void Start()
    {
        // Check if a save file exists, and disable Load Game button if none exists
        if (!SaveSystem.DoesSaveFileExist())
        {
            loadGameButton.interactable = false; // Disable Load Game button
        }

        // Keep the game paused on start to display the main menu
        
    }

    void Update()
    {
        // Listen for the Escape key to toggle pause when the game is running
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

    // Start the game, disable the menu, and resume time
    public void StartGame()
    {
        Resume(); // Resume the game

        // Ensure that waves start only after resuming the game
        //if (waveManager.currentWaveIndex == 0)
        //{
        //    waveManager.StartCoroutine(waveManager.StartWaveRoutine());
        //}
    }

    // Resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the menu
        Time.timeScale = 1f; // Resume normal time progression
        gameIsPaused = false;
    }

    // Pause the game and show the menu
    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the menu
        Time.timeScale = 0f; // Freeze the game time
        gameIsPaused = true;
    }

    // Load the saved game state
    public void LoadGame()
    {
        if (SaveSystem.DoesSaveFileExist()) // Extra check before loading
        {
            saveManager.LoadGame(); // Load the saved data
            Resume(); // Resume the game after loading
        }
        else
        {
            Debug.LogWarning("No saved game found!");
        }
    }

    // Save the current game state
    public void SaveGame()
    {
        saveManager.SaveGame();
        Debug.Log("Game Saved!");
        loadGameButton.interactable = true; // Enable Load Game button after saving
    }

    // Reset the game to default values
    public void ResetGame()
    {
        // Reset wave index, player health, and power-ups to default values
        waveManager.currentWaveIndex = 0; // Reset to first wave
        playerHealth.health = 10; // Reset to full health
        powerUpManager.currentBullet1PowerUpLevel = 0; // Reset power-up levels
        powerUpManager.currentBullet2PowerUpLevel = 0;
        powerUpManager.currentBullet3PowerUpLevel = 0;

        waveManager.ResetWave();
        waveManager.ContinueFromLoadedWave();


        Resume(); // Start the game after resetting
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}


