using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float Speed = 5;

    private Rigidbody2D body;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        this.LookAtTarget();
        this.WalkForward();
    }

    private void LookAtTarget()
    {
        var direction = new Vector3(0, 0, 0) - this.transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void WalkForward()
    {
        //body.velocity = new Vector2(Speed, Speed) * this.transform.forward;
        this.transform.Translate(new Vector3(0, Speed * Time.deltaTime, 0), Space.Self);
    }
}
