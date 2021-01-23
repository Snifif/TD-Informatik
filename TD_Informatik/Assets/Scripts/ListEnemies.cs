using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEnemies : MonoBehaviour
{
    public static List<GameObject> enemies = new List<GameObject>();

    public void Start()
    {
        enemies.Clear();
    }
}
