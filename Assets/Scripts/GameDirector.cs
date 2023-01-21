using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        Instantiate(Player, new Vector3(-5,0,0), Quaternion.identity);
        lastEnemySpawn.Start();

        Home = GameObject.FindGameObjectsWithTag(Enum.GetName(typeof(Tags), Tags.Home)).FirstOrDefault();

        var dist = (transform.position - Camera.main.transform.position).z;
        this.screenWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x + 2;
        this.screenHeight = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y + 2;

        this.UpdateWaveCounterText();
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
            this.waveEnemyCount == this.waveSpawnedEnemyCount &&
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
        winText.SetActive(true);
    }

    private void NextWave()
    {
        if (this.waveCounter == this.maxWaveCount)
        {
            isGameOver = true;
            PerformWin();
            return;
        }

        this.waveCounter++;
        this.waveSpawnedEnemyCount = 0;
        this.waveEnemyCount = (float)(10 * Math.Pow(1.3, (double)waveCounter - 1));

        this.UpdateWaveCounterText();
    }

    private void UpdateWaveCounterText()
    {
        var waveCounterText = this.waveCounterText.GetComponent<TextMeshProUGUI>();
        waveCounterText.text = "Welle: " + waveCounter + " / " + maxWaveCount;
    }
}
