using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> footstepSounds;
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioClip enemyDeathSound1;
    [SerializeField] private AudioClip enemyDeathSound2;
    [SerializeField] private AudioClip buildingBreakdownSound;

    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource walkingSoundAudioSource;
    
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
    private AudioSource CreateDefaultAudioSource()
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        return audioSource;
    }

    public void PlayGunshot()
    {
        var audioSource = CreateDefaultAudioSource();
        audioSource.clip = gunShotSound;
        audioSource.Play();
    }

    public void PlayFootStepSound()
    {
        if (walkingSoundAudioSource.isPlaying) 
            return;

        var random = new System.Random();
        var next = random.Next(footstepSounds.Count);
        walkingSoundAudioSource.clip = footstepSounds[next];
            
        walkingSoundAudioSource.Play();
    }

    public void PlayEnemyDeathSound()
    {
        var audioSource = CreateDefaultAudioSource();
        audioSource.clip = enemyDeathSound2;
        audioSource.Play();
    }

    public void PlayBaseCollapseSound()
    {
        var audioSource = CreateDefaultAudioSource();
        audioSource.clip = buildingBreakdownSound;
        audioSource.Play();
    }
    




    public void Stop()
    {
        //_audioSource.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
