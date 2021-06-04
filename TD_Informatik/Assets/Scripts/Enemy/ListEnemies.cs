using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEnemies : MonoBehaviour
{
    public static List<GameObject> enemies = new List<GameObject>();
    // public static List<EnemyBehavior2> enemyList = new List<EnemyBehavior2>();

    public void Start()
    {
        enemies.Clear();
        // enemyList.Clear();
    }
}
