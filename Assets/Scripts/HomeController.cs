using UnityEngine;

public class HomeController : MonoBehaviour
{
	private GameDirector director;
	
	private void Start()
	{
		director = GameObject.Find("GameDirector").GetComponent<GameDirector>();
	}

	private void OnDestroy()
	{
		director.PerformGameOver();
	}
}