using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject Enemy;
    public static List<GameObject> EnemyCount = new List<GameObject>();    
    
    public float Health;
    public float Damage;
    public float Speed;
    public float KillReward;

    private void InitializeEnemy()
    {
        GameObject newEnemy = Instantiate(Enemy);
        GameObject NodeCoordinates = GenerateMap.PathNodes[0];
        float x = NodeCoordinates.transform.position.x;
        float z = NodeCoordinates.transform.position.z;
        newEnemy.transform.position = new Vector3(x, 1, z);

    }

    private void MoveEnemy()
    {


    }



}
