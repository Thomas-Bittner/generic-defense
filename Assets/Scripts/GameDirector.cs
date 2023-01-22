using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Enemies;
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject waveCounterText;
    public GameObject explosion;

    public bool isGameOver;

    private float enemySpawnCooldown = 1000;
    private Stopwatch lastEnemySpawn = new Stopwatch();
    private float enemySpawnSpreadLimit = 1.2f;
    private GameObject Home;

    private float screenWidth;
    private float screenHeight;

    [SerializeField]
    private int waveCounter = 1;
    private int maxWaveCount = 10;
    private int waveSpawnedEnemyCount;
    private float waveEnemyCount = 10;
    private bool nextWaveTriggered;

    private GameObject WaveUpDialog;
    private List<Button> upgradeButtons = new List<Button>();
    private GameObject player;
    private List<SpecialUpgrades> activeOneTimeUpgrades = new List<SpecialUpgrades>();

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        player = Instantiate(Player, new Vector3(-5,0,0), Quaternion.identity);
        lastEnemySpawn.Start();

        Home = GameObject.FindGameObjectsWithTag(Enum.GetName(typeof(Tags), Tags.Home)).FirstOrDefault();

        var dist = (transform.position - Camera.main.transform.position).z;
        this.screenWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x + 2;
        this.screenHeight = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y + 2;

        this.UpdateWaveCounterText();

        WaveUpDialog = GameObject.FindGameObjectWithTag(Enum.GetName(typeof(Tags), Tags.WaveUpDialog));
        upgradeButtons = FindObjectsOfType<Button>().ToList();
        WaveUpDialog.SetActive(false);
    }

    public void Update()
    {
        if (!isGameOver &&
            lastEnemySpawn.ElapsedMilliseconds > enemySpawnCooldown &&
            this.waveSpawnedEnemyCount < this.waveEnemyCount)
        {
            lastEnemySpawn.Restart();
            this.SpawnEnemy(UnityEngine.Random.Range(0, 3));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        if (!isGameOver &&
            !nextWaveTriggered &&
            this.waveEnemyCount <= this.waveSpawnedEnemyCount &&
            GameObject.FindGameObjectsWithTag(Enum.GetName(typeof(Tags), Tags.Enemy)).Length == 0)
        {
            this.NextWave();
        }
    }

    private void SpawnEnemy(int spawn)
    {
        this.waveSpawnedEnemyCount++;

        float enemySpawnSpread;
        float enemySpawnPosition;
        GameObject enemyToSpawn = this.Enemies[0];
        GameObject enemy;

        if (spawn % 2 == 0)
        {
            enemySpawnSpread = UnityEngine.Random.Range(screenWidth / enemySpawnSpreadLimit, this.screenWidth / enemySpawnSpreadLimit * -1);
            enemySpawnPosition = this.screenHeight;

            // Spawn Down
            if (spawn == 2)
            {
                enemySpawnPosition *= -1;
            }

            enemy = Instantiate(enemyToSpawn, new Vector3(enemySpawnSpread, enemySpawnPosition, 0), Quaternion.identity);
        }
        // Spawn Left
        else
        {
            enemySpawnSpread = UnityEngine.Random.Range(screenHeight / enemySpawnSpreadLimit, this.screenHeight / enemySpawnSpreadLimit * -1);
            enemySpawnPosition = this.screenWidth;

            enemyToSpawn = this.Enemies[1];
            enemySpawnPosition *= -1;

            enemy = Instantiate(enemyToSpawn, new Vector3(enemySpawnPosition, enemySpawnSpread, 0), Quaternion.identity);
        }

        var enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
        enemyBehaviour.Target = this.Home;
    }

    public void PerformGameOver()
    {
        isGameOver = true;
        explosion.SetActive(true);
        gameOverText.SetActive(true);
    }

    public void PerformWin()
    {
        var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
        audioPlayer.PlayGameWonSound();

        winText.SetActive(true);
    }

    private void NextWave()
    {
        nextWaveTriggered = true;

        if (this.waveCounter == this.maxWaveCount)
        {
            isGameOver = true;
            PerformWin();
            return;
        }

        var audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioPlayer>();
        audioPlayer.PlayNextWaveSound();
        
        UpgradeHandle();
    }

    private void UpdateWaveCounterText()
    {
        var waveCounterText = this.waveCounterText.GetComponent<TextMeshProUGUI>();
        waveCounterText.text = "Welle: " + waveCounter + " / " + maxWaveCount;
    }

    private void UpgradeHandle()
    {
        WaveUpDialog.SetActive(true);
        Cursor.visible = true;

        var upgradeEnumList = Enum.GetValues(typeof(Upgrades)).OfType<Upgrades>().ToList();
        var specialUpgradeEnumList = Enum.GetValues(typeof(SpecialUpgrades)).OfType<SpecialUpgrades>().ToList();
        specialUpgradeEnumList = specialUpgradeEnumList.Except(activeOneTimeUpgrades).ToList();

        foreach (var button in upgradeButtons)
        {
            button.onClick.RemoveAllListeners();
            var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            var buttonStar = button.GetComponentsInChildren<Image>().Where(x => x.name.Contains("Special")).First();

            var specialRange = UnityEngine.Random.Range(0, 10);

            if (specialRange == 7)
            {
                buttonStar.gameObject.SetActive(true);

                var specialUpgradeEnumIndex = UnityEngine.Random.Range(0, specialUpgradeEnumList.Count);
                var specialUpgradeEnumValue = specialUpgradeEnumList[specialUpgradeEnumIndex];

                switch (specialUpgradeEnumValue)
                {
                    case SpecialUpgrades.EmergencyRepair:
                        buttonText?.SetText("Notfallreparatur");
                        button.onClick.AddListener(new UnityAction(SpecialUpgradeEmergencyRepair));
                        break;
                    case SpecialUpgrades.QuickFireShots:
                        buttonText?.SetText("Schnellfeuer");
                        button.onClick.AddListener(new UnityAction(SpecialUpgradeQuickFireShots));
                        break;
                    case SpecialUpgrades.ExtremeRange:
                        buttonText?.SetText("Extreme Reichweite");
                        button.onClick.AddListener(new UnityAction(SpecialUpgradeExtremeRange));
                        break;
                    default:
                        break;
                }

                continue;
            }
            
            var upgradeEnumIndex = UnityEngine.Random.Range(0, upgradeEnumList.Count);
            var upgradeEnumValue = upgradeEnumList[upgradeEnumIndex];
            buttonStar.gameObject.SetActive(false);

            switch (upgradeEnumValue)
            {
                case Upgrades.MovementSpeed:
                    buttonText?.SetText("Geschwindigkeit");
                    button.onClick.AddListener(new UnityAction(UpgradeMovementSpeed));
                    break;
                case Upgrades.Range:
                    buttonText?.SetText("Reichweite");
                    button.onClick.AddListener(new UnityAction(UpgradeRange));
                    break;
                default:
                    break;
            }

            upgradeEnumList.RemoveAt(upgradeEnumIndex);
        }
    }

    private void UpgradeMovementSpeed()
    {
        var controller = player.GetComponent<PlayerController>();
        controller.runSpeed *= 1.1f;
        
        CloseWaveUpDialog();
    }

    private void UpgradeRange()
    {
        var controller = player.GetComponent<PlayerController>();
        controller.shootingRange *= 1.1f;

        CloseWaveUpDialog();
    }

    private void SpecialUpgradeEmergencyRepair()
    {
        var homeHealth = Home.GetComponent<HealthBehaviour>();
        homeHealth.Heal(homeHealth.maxHealth);

        CloseWaveUpDialog();
    }

    private void SpecialUpgradeQuickFireShots()
    {
        activeOneTimeUpgrades.Add(SpecialUpgrades.QuickFireShots);

        var controller = player.GetComponent<PlayerController>();
        controller.rateOfFire = 10f;

        CloseWaveUpDialog();
    }

    private void SpecialUpgradeExtremeRange()
    {
        activeOneTimeUpgrades.Add(SpecialUpgrades.ExtremeRange);

        var controller = player.GetComponent<PlayerController>();
        controller.shootingRange = 100f;

        CloseWaveUpDialog();
    }

    private void CloseWaveUpDialog()
    {
        Time.timeScale = 1;
        WaveUpDialog.SetActive(false);
        Cursor.visible = false;

        this.waveCounter++;
        this.waveSpawnedEnemyCount = 0;
        this.waveEnemyCount = (float)(10 * Math.Pow(1.3, (double)waveCounter - 1));

        this.UpdateWaveCounterText();
        nextWaveTriggered = false;
    }
}
