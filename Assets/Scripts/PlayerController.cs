using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    [SerializeField] private AudioSource walkingSoundAudioSource;
    [SerializeField] private AudioSource shootingSoundAudioSource;
    public List<AudioClip> footstepSounds;

    private float horizontalMovement;
    private float verticalMovement;
    private const float moveLimiter = 0.7f;
    
    private bool isShooting;

    public float runSpeed = 10.0f;

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
    }

    public void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        
        if (!isShooting)
        {
            isShooting = Input.GetMouseButtonDown(0);
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
            PlayWalkingSound();
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

            shootingSoundAudioSource.Play();
        }
    }

    private void PlayWalkingSound()
    {
        if (walkingSoundAudioSource.isPlaying) 
            return;

        var random = new System.Random();
        var next = random.Next(footstepSounds.Count);
        walkingSoundAudioSource.clip = footstepSounds[next];
            
        walkingSoundAudioSource.Play();
    }

    private void Animate(bool isWalking)
    {
        this.elvisWalk.SetActive(isWalking);
        this.elvisIdle.SetActive(!isWalking);
    }
}
