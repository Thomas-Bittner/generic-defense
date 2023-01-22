using System;
using System.Diagnostics;
using UnityEngine;

public class PackageBehaviour : MonoBehaviour
{
    private GameObject house;

    private float timeToDecay = 5000;
    private Stopwatch aliveTime = new Stopwatch();

    private void Start()
    {
        house = GameObject.FindGameObjectWithTag(Enum.GetName(typeof(Tags), Tags.Home));
        aliveTime.Start();
    }

    private void Update()
    {
        if (aliveTime.ElapsedMilliseconds > timeToDecay)
        {
            var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
            audioPlayer.PlayPackageDeSpawnSound();

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(Enum.GetName(typeof(Tags), Tags.Player)))
        {
            var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
            audioPlayer.PlayPackagePickUpSound();

            house.SendMessage("Heal", 1);
            Destroy(this.gameObject);
        }
    }
}
