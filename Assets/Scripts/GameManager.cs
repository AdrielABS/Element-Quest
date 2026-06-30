using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Respawn")]
    public float restartDelay = 2f;

    private bool restarting = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RestartLevel()
    {
        if (!restarting)
            StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {
        restarting = true;

        yield return new WaitForSeconds(restartDelay);

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}