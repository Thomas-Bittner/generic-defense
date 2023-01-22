using UnityEngine;

public class HomeController : MonoBehaviour
{
	private GameDirector director;
	
	private void Start()
	{
		director = FindObjectOfType<GameDirector>();
	}

	private void OnDestroy()
	{
        var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
        audioPlayer.PlayBuildingBreakdownSound();
		audioPlayer.PlayMenuMusic();

		director.PerformGameOver();
	}
}