using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
