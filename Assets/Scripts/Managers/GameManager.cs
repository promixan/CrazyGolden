using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    }

    void Start()
    {
        StartGame();
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
        gameActive = true;
        spawnManager.StartNewGame();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameActive = false;
    }

    public void ReturnToMenu()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        gamePaused = false;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        gamePaused = false;
        gameActive = false;
        spawnManager.ResetGame();
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        StartGame();
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
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            gamePaused = false;
            pauseScreen.SetActive(false);
        }
    }
}
