using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float Speed = 5;
    public GameObject Target;

    private float Health = 1;
    private Rigidbody2D body;

    public void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        this.LookAtTarget();
        this.WalkForward();
    }

    private void LookAtTarget()
    {
        var direction = this.Target.transform.position - this.transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void WalkForward()
    {
        var force = (this.Target.transform.position - this.transform.position).normalized;
        this.body.velocity = force;
        //this.transform.Translate(new Vector3(0, Speed * Time.deltaTime, 0), Space.Self);
    }

    private void Damage(float damage)
    {
        this.Health -= damage;

        if (this.Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
