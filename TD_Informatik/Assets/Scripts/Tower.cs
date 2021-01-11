using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int updateMode = 0;
    private float nextAttackTime;

    private GameObject currentTarget;

    private void Start()
    {
        nextAttackTime = Time.time;
    }

    private void updateClosestEnemy() // wird ausgeführt werden, wenn Türme so eingestellt sind, dass sie den nächsten Gegner angreifen sollen (sucht nach dem Gegner mit der geringsten Entfernung zum Turm
    {
        GameObject nearestEnemy = null;

        float distance = Mathf.Infinity;

        foreach(GameObject gegner in ListEnemies.enemies)
        {
            float distance2 = (transform.position - gegner.transform.position).magnitude;

            if(distance2 < distance)
            {
                distance = distance2;
                nearestEnemy = gegner;
            }
        }

        if (distance <= range)
        {
            currentTarget = nearestEnemy;
        }
        else
        {
            currentTarget = null;
        }
        
    }

    private void updateFurthestEnemy() // wird ausgeführt werden, wenn die Türme so eingestellt sind, dass sie den Gegner angreifen sollen, der dem Ziel am nächsten ist
    {
        GameObject furthestEnemy = null;

        float distance = range;
        int highestIndex = 0;
        float distanceToPathNode = Mathf.Infinity;

        foreach(GameObject gegner in ListEnemies.enemies)
        {
            float distance2 = (transform.position - gegner.transform.position).magnitude; //distance2 speichert den Abstand des Turms zum Gegner
            for (int i = 0; i < GenerateMap.PathNodes.Count; i++) //sucht die aktuelle Streckenposition des Gegners (in highestIndex gespeichert)
            {
                Vector3 currentPathNodePos = new Vector3(GenerateMap.PathNodes[i].transform.position.x, 1, GenerateMap.PathNodes[i].transform.position.z);
                if((gegner.transform.position - currentPathNodePos).magnitude < 2)
                {
                    Vector3 nextPathNodePos = new Vector3();
                    if(i != GenerateMap.PathNodes.Count - 1)
                    {
                        nextPathNodePos = new Vector3(GenerateMap.PathNodes[i + 1].transform.position.x, 1, GenerateMap.PathNodes[i + 1].transform.position.z); // kann eventuell Probleme machen
                    }
                    else
                    {
                        nextPathNodePos = currentPathNodePos;
                        Debug.Log("nextPathNodePos = currentPathNodePos");
                    }
                    
                    if (i > highestIndex || ((i == highestIndex) && (gegner.transform.position - nextPathNodePos).magnitude < distanceToPathNode))
                    {
                        if (distance2 <= distance)
                        {
                            highestIndex = i;
                            distanceToPathNode = (gegner.transform.position - nextPathNodePos).magnitude;
                            i = GenerateMap.PathNodes.Count;
                            furthestEnemy = gegner;
                        }
                    }
                }
            }
        }

        if(furthestEnemy != null)
        {
            currentTarget = furthestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }

    private void shoot()
    {
         EnemyBehavior2 enemyScript = currentTarget.GetComponent<EnemyBehavior2>();
         enemyScript.takeDamage(damage);
    }

    private void Update()
    {
        if (updateMode == 0)
        {
            updateClosestEnemy();
        }
        if(updateMode == 1)
        {
            updateFurthestEnemy();
        }
        if(Time.time >= nextAttackTime)
        {
            if(currentTarget != null)
            {
                shoot();
                nextAttackTime = Time.time + attackSpeed;
            }
        }
    }
}
