using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQ : MonoBehaviour
{
    public int HQHealth;
    public int Money;


    public List<GameObject> EnemyList = new List<GameObject>();


    private void Update()
    {
        if (HQHealth <= 0)
        {
            // DIsable ui und game over screen einfügen
        }
    }

}
