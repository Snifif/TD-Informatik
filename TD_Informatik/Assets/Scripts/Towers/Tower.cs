using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int updateMode = 1;
    
    private float nextAttackTime;
    public float turnspeed = 15f;

    private GameObject currentTarget;

    private void Start()
    {
        nextAttackTime = Time.time;

    }

    private void updateClosestEnemy() // wird ausgeführt werden, wenn Türme so eingestellt sind, dass sie den nächsten Gegner angreifen sollen (sucht nach dem Gegner mit der geringsten Entfernung zum Turm
    {
        GameObject nearestEnemy = null;

        float distance = Mathf.Infinity;

        foreach (GameObject gegner in ListEnemies.enemies)
        {
            float distance2 = (transform.position - gegner.transform.position).magnitude;

            if (distance2 < distance)
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

    private void updateFurthestEnemy()
    {
        GameObject target = null;
        int highestIndexOfTarget = 0;
        float highestDistanceToNextNode = 0;

        foreach (GameObject gegner in ListEnemies.enemies)
        {
            float distance = (transform.position - gegner.transform.position).magnitude;
            if (distance <= range)
            {
                EnemyBehavior2 enemyScript = gegner.GetComponent<EnemyBehavior2>(); //machte Probleme
                int targetIndex = 0;
                float distanceToTargetNode = 0;
                targetIndex = enemyScript.indexOfTargetNode;//machte auch Probleme
                GameObject targetNode = GenerateMap.PathNodes[targetIndex];
                distanceToTargetNode = (gegner.transform.position - targetNode.transform.position).magnitude;
                if(((targetIndex == highestIndexOfTarget) && (distanceToTargetNode >= highestDistanceToNextNode)) || (targetIndex > highestIndexOfTarget))
                {
                    highestIndexOfTarget = targetIndex;
                    highestDistanceToNextNode = distanceToTargetNode;
                    target = gegner;
                }
                
                
            }
        }

        currentTarget = target;

    }
    
    private void shoot()
    {
        EnemyBehavior2 enemyScript = currentTarget.GetComponent<EnemyBehavior2>();
        enemyScript.takeDamage(damage);
    }

    private void OnMouseDown()   // wenn drauf ge clickt wird updatemode ändern
    {

    }

    private void RotateToEnemy()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.transform.position - this.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
            this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        
    }

    public virtual void SetTurnSpeed()
    {
        turnspeed = 15f;
    }

    private void Update()
    {
        if (updateMode == 0)
        {
            updateClosestEnemy();
        }
        if (updateMode == 1)
        {
            updateFurthestEnemy();
        }
        if (Time.time >= nextAttackTime)
        {
            if (currentTarget != null)
            {
                shoot();
                nextAttackTime = Time.time + attackSpeed;
            }
        }

        RotateToEnemy();
    }
}
