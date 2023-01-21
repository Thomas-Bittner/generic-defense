using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Enemies;

    private float enemySpawnCooldown = 1000;
    private Stopwatch lastEnemySpawn = new Stopwatch();
    private float enemySpawnSpreadLimit = 1.2f;
    private GameObject Home;

    private float screenWidth;
    private float screenHeight;

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
    }

    public void Update()
    {
        if (lastEnemySpawn.ElapsedMilliseconds > enemySpawnCooldown)
        {
            lastEnemySpawn.Restart();
            this.SpawnEnemy(UnityEngine.Random.Range(0, 3));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }

    private void SpawnEnemy(int spawn)
    {
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
}
