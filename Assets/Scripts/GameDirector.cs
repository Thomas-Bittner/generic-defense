using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Enemies;

    private float enemySpawnCooldown = 1000;
    private Stopwatch lastEnemySpawn = new Stopwatch();
    private float enemySpawnSpreadLimit = 1.2f;

    private float cameraWidth;
    private float cameraHeight;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Instantiate(Player, new Vector3(-10,0,0), Quaternion.identity);
        lastEnemySpawn.Start();

        this.cameraWidth = Camera.main.pixelWidth / Camera.main.orthographicSize;
        this.cameraHeight = Camera.main.pixelWidth / Camera.main.orthographicSize;
    }

    public void Update()
    {
        if (lastEnemySpawn.ElapsedMilliseconds > enemySpawnCooldown)
        {
            lastEnemySpawn.Reset();
            lastEnemySpawn.Start();

            this.SpawnEnemy(Random.Range(0, 4));
        }
    }

    private void SpawnEnemy(int spawn)
    {
        float enemySpawnSpread;
        float enemySpawnPosition;
        GameObject enemyToSpawn = this.Enemies[0];

        if (spawn % 2 == 0)
        {
            enemySpawnSpread = Random.Range(cameraWidth / enemySpawnSpreadLimit, this.cameraWidth / enemySpawnSpreadLimit * -1);
            enemySpawnPosition = this.cameraHeight;

            if (spawn == 2)
            {
                enemySpawnPosition *= -1;
            }

            Instantiate(enemyToSpawn, new Vector3(enemySpawnSpread, enemySpawnPosition, 0), Quaternion.identity);
        }
        else
        {
            enemySpawnSpread = Random.Range(cameraHeight / enemySpawnSpreadLimit, this.cameraHeight / enemySpawnSpreadLimit * -1);
            enemySpawnPosition = this.cameraWidth;

            if (spawn == 1)
            {
                enemyToSpawn = this.Enemies[0];
            }
            else if (spawn == 3)
            {
                enemySpawnPosition *= -1;
                enemyToSpawn = this.Enemies[0];
            }

            Instantiate(enemyToSpawn, new Vector3(enemySpawnPosition, enemySpawnSpread, 0), Quaternion.identity);
        }
    }
}
