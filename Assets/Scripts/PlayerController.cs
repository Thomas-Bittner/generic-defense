using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    private float horizontalMovement;
    private float verticalMovement;
    private bool isShooting;
    private const float moveLimiter = 0.7f;

    public float runSpeed = 7.0f;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        // Gives a value between -1 and 1
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        isShooting = Input.GetMouseButtonDown(0);
    }

    public void FixedUpdate()
    {
        this.MovePlayer();
        this.Shoot();
    }

    private void MovePlayer()
    {
        if (horizontalMovement != 0 && verticalMovement != 0)
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontalMovement *= moveLimiter;
            verticalMovement *= moveLimiter;
        }

        body.velocity = new Vector2(horizontalMovement * runSpeed, verticalMovement * runSpeed);
    }

    private void Shoot()
    {
        if (this.isShooting)
        {
        }
    }
}
