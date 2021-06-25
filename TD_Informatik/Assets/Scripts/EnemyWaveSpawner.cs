using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    public static Text Wavee;

    int WaveCount = 1;
    int EnemySpawnAmount = 5;
    int Enemy2SpawnAmount = 0;
    int EnemiesSpawned;
    int Enemies2Spawned;

    float WaveTimer = 2f;
    float TimeWaited;
    public bool checkwave = true;
    void Start()
    {

    }

    void Update()
    {
        Wavee = GetComponent<Text>();
        Wave();
        CheckEnemies();
    }

    void Wave()
    {
        if (checkwave == true)    // erst spawnen alle vorherigen tot sind
        {

            if (WaveTimer <= TimeWaited)
            {
                if (EnemiesSpawned != EnemySpawnAmount)
                {
                    TimeWaited = 0;
                    Instantiate(Enemy1, new Vector3(GenerateMap.startTile.transform.position.x, 1, GenerateMap.startTile.transform.position.z), Quaternion.identity);
                    EnemiesSpawned++;
                }
                else
                {
                    if (Enemies2Spawned == Enemy2SpawnAmount)
                    {
                        checkwave = false;
                        EnemiesSpawned = 0;
                        Enemies2Spawned = 0;
                        EnemySpawnAmount += 5;
                        Enemy2SpawnAmount += 1;
                        WaveTimer -= 0.2f * WaveTimer;
                    }
                    else
                    {
                        Instantiate(Enemy2, new Vector3(GenerateMap.startTile.transform.position.x, 1, GenerateMap.startTile.transform.position.z), Quaternion.identity);
                        Enemies2Spawned++;
                    }
                }
            }
            TimeWaited += Time.deltaTime;
        }
    }

    public void CheckEnemies()
    {
        if (ListEnemies.enemies.Count == 0 && checkwave == false)       // wenn alle tot sind
        {
            checkwave = true;
            Money.money += 50; // Wave Gewonnen -> Geld bekommen
            Points.points += 100;
            WaveCount++;
            Wavee.text = "Wave " + WaveCount.ToString();
            EnemyBehavior2.speed += 0.1f; // Gegner werden schneller

        }
    }
}

