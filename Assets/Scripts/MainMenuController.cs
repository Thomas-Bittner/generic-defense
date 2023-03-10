using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject creditsCanvas;
    public GameObject moveMessage;

    private void Start()
    {
        var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
        audioPlayer.PlayMenuMusic();
    }

    public void StartGame()
    {
        var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
        audioPlayer.PlayStartGameSound();

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
