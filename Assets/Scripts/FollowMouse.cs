using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public void Update()
    {
        var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
