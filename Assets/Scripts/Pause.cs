using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Menu")]
    public GameObject pausePanel;
    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        playerMovement.enabled = false;
        playerAttack.enabled = false;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        isPaused = true;
    }

    public void Resume()
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;

        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        Application.Quit();
    }
}