using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject Player;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Instantiate(Player, new Vector3(-10,0,0), Quaternion.identity);
    }
}
