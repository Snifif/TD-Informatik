using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;

    int WaveCount = 1;
    int EnemySpawnAmount = 5;
    int EnemiesSpawned;

    float WaveTimer = 3f;
    float TimeWaited;
    public bool checkwave = true;
    void Start()
    {

    }

    void Update()
    {
        Wave();
        CheckEnemies();
    }

    void Wave()
    {
        if (checkwave == true)
        {

            if (WaveTimer <= TimeWaited)
            {
                TimeWaited = 0;
                Instantiate(Enemy1, new Vector3(GenerateMap.startTile.transform.position.x, 1, GenerateMap.startTile.transform.position.z), Quaternion.identity);
                EnemiesSpawned++;
                if (EnemiesSpawned == EnemySpawnAmount)
                {
                    checkwave = false;
                    EnemiesSpawned = 0;
                    EnemySpawnAmount += 5;
                    WaveTimer -= 0.2f * WaveTimer;
                }
            }
            TimeWaited += Time.deltaTime;
        }
    }

    void CheckEnemies()
    {
        if (ListEnemies.enemies.Count == 0 && checkwave == false)
        {
            checkwave = true;
            WaveCount++;
        }
    }
}
