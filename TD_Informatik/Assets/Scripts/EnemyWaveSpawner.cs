using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;

    int WaveCount = 1;
    public bool checkwave = true;
    void Start()
    {

    }

    void Update()
    {
        Wave();
        ExecuteAfterTime(1f);
        CheckEnemies();
    }

    void Wave()
    {
        if (checkwave == true)
        {
            ExecuteAfterTime(1000f);
            int EnemySpawnAmount = 5;

            for (int i = 0; i <= EnemySpawnAmount; i++)
            {
                GameObject newenemy = Instantiate(Enemy1);
                newenemy.transform.position = new Vector3(GenerateMap.startTile.transform.position.x, 1, GenerateMap.startTile.transform.position.z);
                ExecuteAfterTime(1.5f);

            }
            WaveCount++;
            EnemySpawnAmount = EnemySpawnAmount + 3;
            checkwave = false;
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

    }

    void CheckEnemies()
    {
        if (ListEnemies.enemies.Count == 0)
        {
            checkwave = true;
        }
    }
}
