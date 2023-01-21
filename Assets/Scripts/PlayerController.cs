using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

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

    public float runSpeed = 7.0f;

    public GameObject PrefabShot;
    public GameObject PrefabCrosshair;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Instantiate(PrefabCrosshair);
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

        if (body.velocity.magnitude > 0)
        {
            PlayWalkingSound();
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
}
