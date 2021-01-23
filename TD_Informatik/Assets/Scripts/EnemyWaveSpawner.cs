﻿using System.Collections;
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
    int EnemiesSpawned;

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
                TimeWaited = 0;
                Instantiate(Enemy1, new Vector3(GenerateMap.startTile.transform.position.x, 1, GenerateMap.startTile.transform.position.z), Quaternion.identity);
                EnemiesSpawned++;
                if (EnemiesSpawned == EnemySpawnAmount)  // wenn alle gespawnt sind das untere machen
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
