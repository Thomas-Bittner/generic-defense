using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject creditsCanvas;

    void Start()
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
        var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
        audioPlayer.PlayMenuMusic();

        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
