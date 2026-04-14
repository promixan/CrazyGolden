using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    private SpawnManager spawnManager;

    private bool gameActive = false;
    private bool gamePaused = false;

    private InputAction pauseAction;

    void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        mainScreen.SetActive(true);
    }

    void Update()
    {
        if (pauseAction.WasReleasedThisFrame() && gameActive)
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        mainScreen.SetActive(false);
        gameActive = true;
        spawnManager.StartNewGame();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameActive = false;
    }

    public void BackToMainScreen()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        gamePaused = false;
        spawnManager.ResetGame();
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gameActive = false;
        mainScreen.SetActive(true);
    }

    public bool IsGameOver()
    {
        return !gameActive;
    }

    public bool IsGameActive()
    {
        return gameActive;
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }

    void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            gamePaused = true;
            pauseScreen.SetActive(true);
        } else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            gamePaused = false;
            pauseScreen.SetActive(false);
        }
    }
}
