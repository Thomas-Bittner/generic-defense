using System;
using System.Diagnostics;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class EnemyBehaviour : MonoBehaviour
{
    private float Speed = 3;
    private float Damage = 1;
    public GameObject Target;

    private Rigidbody2D body;
    private float attackSpeed = 1000;
    private Stopwatch attackTimer = new Stopwatch();
    private GameDirector director;

    public void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
        director = FindObjectOfType<GameDirector>();
        attackTimer.Start();
    }

    public void Update()
    {
        if (director.isGameOver)
            return;
        
        LookAtTarget();
        WalkForward();
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
        this.body.velocity = force * this.Speed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(Enum.GetName(typeof(Tags), Tags.Home)) &&
            this.attackTimer.ElapsedMilliseconds > attackSpeed)
        {
            this.attackTimer.Restart();
            collision.gameObject.SendMessage("Damage", this.Damage);
        }
    }

    void OnDestroy()
    {
        if (!director.isGameOver)
        {
            var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();

            if (gameObject.name.Contains("Enemy0"))
            {
                audioPlayer.PlayEnemy0DeathSound();
            }
            else
            {
                audioPlayer.PlayEnemy1DeathSound();
            }
        }
    }
}
