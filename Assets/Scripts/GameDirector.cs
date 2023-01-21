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

    private float cameraWidth;
    private float cameraHeight;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        Instantiate(Player, new Vector3(-5,0,0), Quaternion.identity);
        lastEnemySpawn.Start();

        Home = GameObject.FindGameObjectsWithTag(Enum.GetName(typeof(Tags), Tags.Home)).FirstOrDefault();

        this.cameraWidth = Camera.main.pixelWidth / Camera.main.orthographicSize;
        this.cameraHeight = Camera.main.pixelWidth / Camera.main.orthographicSize;
    }

    public void Update()
    {
        if (lastEnemySpawn.ElapsedMilliseconds > enemySpawnCooldown)
        {
            lastEnemySpawn.Restart();
            this.SpawnEnemy(UnityEngine.Random.Range(0, 4));
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
            enemySpawnSpread = UnityEngine.Random.Range(cameraWidth / enemySpawnSpreadLimit, this.cameraWidth / enemySpawnSpreadLimit * -1);
            enemySpawnPosition = this.cameraHeight;

            // Spawn Down
            if (spawn == 2)
            {
                enemySpawnPosition *= -1;
            }

            enemy = Instantiate(enemyToSpawn, new Vector3(enemySpawnSpread, enemySpawnPosition, 0), Quaternion.identity);
        }
        else
        {
            enemySpawnSpread = UnityEngine.Random.Range(cameraHeight / enemySpawnSpreadLimit, this.cameraHeight / enemySpawnSpreadLimit * -1);
            enemySpawnPosition = this.cameraWidth;

            // Spawn Right
            if (spawn == 1)
            {
                //enemyToSpawn = this.Enemies[0];
            }
            // Spawn Left
            else if (spawn == 3)
            {
                //enemySpawnPosition *= -1;
                enemyToSpawn = this.Enemies[1];
            }

            enemySpawnPosition *= -1;

            enemy = Instantiate(enemyToSpawn, new Vector3(enemySpawnPosition, enemySpawnSpread, 0), Quaternion.identity);
        }

        var enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
        enemyBehaviour.Target = this.Home;
    }
}
