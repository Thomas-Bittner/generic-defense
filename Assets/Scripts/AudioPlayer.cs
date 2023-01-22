using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footstepSounds;
    [SerializeField] private List<AudioClip> buildingDemolishSounds;
    [SerializeField] private List<AudioClip> enemy0DeathSounds;
    [SerializeField] private List<AudioClip> enemy1DeathSounds;
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioClip buildingBreakdownSound;
    
    [SerializeField] private AudioClip gameWonSound;
    [SerializeField] private AudioClip nextWaveSound;
    [SerializeField] private AudioClip packagePickUpSound;
    [SerializeField] private AudioClip packageDeSpawnSound;
    
    [SerializeField] private AudioClip startGameSound;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource walkingSoundAudioSource;
    [SerializeField] private AudioSource buildingDemolishSoundAudioSource;
    [SerializeField] private AudioSource gunShotAudioSource;
    [SerializeField] private AudioSource packageAudioSource;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource enemyAudioSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayMenuMusic()
    {
        if (musicAudioSource.isPlaying)
            musicAudioSource.Stop();

        musicAudioSource.clip = menuMusic;
        musicAudioSource.Play();
    }

    public void PlayGameMusic(float delay = 0)
    {
        if (musicAudioSource.isPlaying)
            musicAudioSource.Stop();

        musicAudioSource.clip = gameMusic;
        musicAudioSource.PlayDelayed(delay);
    }

    public void PlayStartGameSound()
    {
        gunShotAudioSource.clip = startGameSound;
        gunShotAudioSource.Play();

        PlayGameMusic(0.7f);
    }

    public void PlayGunshot()
    {
        gunShotAudioSource.clip = gunShotSound;
        gunShotAudioSource.Play();
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
        var random = new System.Random();
        var next = random.Next(enemy0DeathSounds.Count);
        
        enemyAudioSource.clip = enemy0DeathSounds[next];
        enemyAudioSource.Play();
    }

    public void PlayEnemy1DeathSound()
    {
        var random = new System.Random();
        var next = random.Next(enemy1DeathSounds.Count);
        
        enemyAudioSource.clip = enemy1DeathSounds[next];
        enemyAudioSource.Play();
    }

    public void PlayBuildingBreakdownSound()
    {
        buildingDemolishSoundAudioSource.clip = buildingBreakdownSound;
        buildingDemolishSoundAudioSource.Play();
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

    public void PlayGameWonSound()
    {
        // Auf den musicAudioSource?

        packageAudioSource.clip = gameWonSound;
        packageAudioSource.Play();
    }

    public void PlayNextWaveSound()
    {
        packageAudioSource.clip = nextWaveSound;
        packageAudioSource.Play();
    }

    public void PlayPackagePickUpSound()
    {
        packageAudioSource.clip = packagePickUpSound;
        packageAudioSource.Play();
    }

    public void PlayPackageDeSpawnSound()
    {
        packageAudioSource.clip = packageDeSpawnSound;
        packageAudioSource.Play();
    }
}
