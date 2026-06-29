using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void BotaoJogar()
    {
        SceneManager.LoadScene("Fasep");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Jogo encerrado");
    }
}