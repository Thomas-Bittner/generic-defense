using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject creditsCanvas;
    public GameObject moveMessage;

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Scene1");
    }
    
    public void ShowCredits()
    {
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void ShowMovedMessage()
    {
        moveMessage.SetActive(true);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
