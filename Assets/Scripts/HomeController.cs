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
		director.PerformGameOver();
	}
}