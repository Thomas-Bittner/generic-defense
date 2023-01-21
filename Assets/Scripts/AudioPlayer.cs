using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footstepSounds;
    [SerializeField] private List<AudioClip> buildingDemolishSounds;
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioClip enemy0DeathSound;
    [SerializeField] private AudioClip enemy1DeathSound;
    [SerializeField] private AudioClip buildingBreakdownSound;

    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource walkingSoundAudioSource;
    [SerializeField] private AudioSource buildingDemolishSoundAudioSource;
    
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

    public void PlayEnemy0DeathSound()
    {
        var audioSource = CreateDefaultAudioSource();
        audioSource.clip = enemy0DeathSound;
        audioSource.Play();
    }

    public void PlayEnemy1DeathSound()
    {
        var audioSource = CreateDefaultAudioSource();
        audioSource.clip = enemy1DeathSound;
        audioSource.Play();
    }

    public void PlayBuildingBreakdownSound()
    {
        var audioSource = CreateDefaultAudioSource();
        audioSource.clip = buildingBreakdownSound;
        audioSource.Play();
    }
    
    public void PlayBuildingDemolishSound()
    {
        if (buildingDemolishSoundAudioSource.isPlaying) 
            return;

        var random = new System.Random();
        var next = random.Next(buildingDemolishSounds.Count);
        buildingDemolishSoundAudioSource.clip = buildingDemolishSounds[next];
            
        buildingDemolishSoundAudioSource.Play();
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
