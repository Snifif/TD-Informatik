using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject Enemy;
    public static List<GameObject> Enemies = new List<GameObject>();    
    
    public float Health;
    public float Damage;
    public float Speed;
    public float KillReward;
    public int Counter = 1;
    private void Start()
    {

        InitializeEnemy();
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void InitializeEnemy()
    {
        GameObject newEnemy = Instantiate(Enemy);
        GameObject NodeCoordinates = GenerateMap.PathNodes[0];
        newEnemy.transform.position = new Vector3(2, 1, 5);
        float x = NodeCoordinates.transform.position.x;
        float z = NodeCoordinates.transform.position.z;

        Enemies.Add(newEnemy);
    }

    private void MoveEnemy()
    {
        int enemycount = Enemies.Count;
        for (int i = 0; i <= enemycount; i++ ) // für jeden Gegner bewegen
        {
            GameObject nextPath = GenerateMap.PathNodes[Counter];
            float x = nextPath.transform.position.x - Enemies[i].transform.position.x;
            float z = nextPath.transform.position.z - Enemies[i].transform.position.z;
            
            if (x> 0.01)
            {
                x = nextPath.transform.position.x - Enemies[i].transform.position.x;
                Enemies[i].transform.position = Vector3.left;
            }
            if (z> 0.01)
            {
                z = nextPath.transform.position.z - Enemies[i].transform.position.z;
                Enemies[i].transform.position = Vector3.up;

            }
            if (x < 0.01)
            {
                x = nextPath.transform.position.x - Enemies[i].transform.position.x;
                Enemies[i].transform.position = Vector3.right;
            }
            if (z < 0.01)
            {
                z = nextPath.transform.position.z - Enemies[i].transform.position.z;
                Enemies[i].transform.position = Vector3.down;

            }
        }

        Counter++;

    }



}
