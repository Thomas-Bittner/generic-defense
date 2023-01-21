using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    public float SelfDestructDistance = 15;
    private Vector3 StartPosition;
    
    public void Start()
    {
        this.StartPosition = this.transform.position;
    }

    public void FixedUpdate()
    {
        if (Vector3.Distance(StartPosition, this.transform.position) >= SelfDestructDistance)
        {
            Destroy(this.gameObject);
        }

        this.transform.Translate(new Vector3(0, 12 * Time.deltaTime, 0), Space.Self);
    }
}
