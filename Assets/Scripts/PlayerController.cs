using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;


    private float horizontalMovement;
    private float verticalMovement;
    private const float moveLimiter = 0.7f;
    
    private bool isShooting;

    private float runSpeed = 10.0f;
    private float rateOfFire = 250f;
    private Stopwatch rateOfFireTimer = new Stopwatch();

    public GameObject PrefabShot;
    public GameObject PrefabCrosshair;
    public GameDirector gameDirector;

    private float screenWidth;
    private float screenHeight;

    private GameObject elvisWalk;
    private GameObject elvisIdle;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        Instantiate(PrefabCrosshair);

        var dist = (transform.position - Camera.main.transform.position).z;
        this.screenWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - 2;
        this.screenHeight = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y - 2;

        this.elvisWalk = transform.Find("elvis walk animation").gameObject;
        this.elvisIdle = transform.Find("elvis").gameObject;

        rateOfFireTimer.Start();
    }

    public void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        
        if (!isShooting)
        {
            isShooting = Input.GetMouseButtonDown(0);
        }

        if (Input.GetMouseButton(0) &&
            rateOfFireTimer.ElapsedMilliseconds > rateOfFire)
        {
            rateOfFireTimer.Restart();
            isShooting = true;
        }
    }

    public void FixedUpdate()
    {
        if (gameDirector.isGameOver)
            return;
        
        MovePlayer();
        LookToCrosshair();
        Shoot();
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

        this.transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x, screenWidth * -1, screenWidth),
            Mathf.Clamp(this.transform.position.y, screenHeight * -1, screenHeight),
            this.transform.position.z);

        if (body.velocity.magnitude > 0)
        {
            var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
            audioPlayer.PlayFootStepSound();
            Animate(true);
        }
        else
        {
            Animate(false);
        }
    }

    private void LookToCrosshair()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(this.transform.position);
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Shoot()
    {
        if (this.isShooting)
        {
            this.isShooting = false;

            var shotSpawn = this.transform.position + (this.transform.forward);
            Instantiate(this.PrefabShot, shotSpawn, this.transform.rotation);

            var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
            audioPlayer.PlayGunshot();
        }
    }

    private void Animate(bool isWalking)
    {
        this.elvisWalk.SetActive(isWalking);
        this.elvisIdle.SetActive(!isWalking);
    }
}
