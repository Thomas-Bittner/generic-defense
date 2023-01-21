using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    private float horizontalMovement;
    private float verticalMovement;
    private bool isShooting;
    private const float moveLimiter = 0.7f;
    private GameObject Crosshair;

    public float runSpeed = 7.0f;

    public GameObject PrefabShot;
    public GameObject PrefabCrosshair;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Crosshair = Instantiate(PrefabCrosshair);
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
        this.LookToCrosshair();
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

    private void LookToCrosshair()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        if (this.isShooting)
        {
            //Instantiate(this.PrefabShot, this.transform.position, this.)
        }
    }
}
