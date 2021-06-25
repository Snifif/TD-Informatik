using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] public float range;
    [SerializeField] public float attackSpeed;
    public float baseRange;
    public float baseAttackSpeed;
    [SerializeField] private int updateMode = 1;
    
    private float nextAttackTime;
    public float turnspeed = 15f;

    private GameObject currentTarget;
    public GameObject ammo;
    public GameObject ammospawner;

    public float damageMultiplier = 1;
    public float attackSpeedMultiplier = 1;
    public float rangeMultiplier = 1;

    public bool multipliersCorrect = true;

    public string turretType;
    public int turretPrice;
    public int turretValue;

    private AudioSource shootsfx;

    private void Awake()
    {
        baseRange = range;
        baseAttackSpeed = attackSpeed;
    }
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
        shootsfx = GetComponent<AudioSource>();
        shootsfx.volume = 0.5f;
        shootsfx.PlayOneShot(shootsfx.clip);

        GameObject NewAmmo = (GameObject)Instantiate(ammo, ammospawner.transform.position, this.transform.rotation);
        AmmoBehaviour AMMO = NewAmmo.GetComponent<AmmoBehaviour>();

        if (AMMO != null)
        {
            AMMO.ConfirmEnemy(currentTarget.transform, damageMultiplier);
        }
    }

    private void OnMouseDown()   // wenn drauf ge clickt wird updatemode ändern
    {
        ButtonTowerInfo.buttonUpdated = false;
        ButtonTowerInfo.buttonInteractable = true;
        ButtonTowerInfo.tower = this.gameObject;
        ButtonTowerInfo.buttonText = turretType + " / " + turretPrice + " / " + damageMultiplier + " / " + attackSpeed + " / " + range;
        ButtonDamage.buttonUpdated = false;
        ButtonDamage.buttonInteractable = true;
        ButtonDamage.tower = this.gameObject;
        ButtonDamage.buttonText = "Upgrade Damage to: " + (damageMultiplier + 1) + " $" + (turretPrice / 2* Mathf.Pow(2, damageMultiplier));
        ButtonAttackSpeed.buttonUpdated = false;
        ButtonAttackSpeed.buttonInteractable = true;
        ButtonAttackSpeed.tower = this.gameObject;
        ButtonAttackSpeed.buttonText = "Upgrade Attack Speed to: " + (baseAttackSpeed / (attackSpeedMultiplier + 1)) + " $" + (turretPrice / 2 * Mathf.Pow(2, attackSpeedMultiplier));
        ButtonAttackRange.buttonUpdated = false;
        ButtonAttackRange.buttonInteractable = true;
        ButtonAttackRange.tower = this.gameObject;
        ButtonAttackRange.buttonText = "Upgrade Attack Range to: " + (baseRange * (rangeMultiplier + 1)) + " $" + (turretPrice / 2 *Mathf.Pow(2, rangeMultiplier));
        ButtonSell.buttonUpdated = false;
        ButtonSell.buttonInteractable = true;
        ButtonSell.tower = this.gameObject;
        ButtonSell.buttonText = "Sell for: " + (turretValue / 2);
        ButtonClose.buttonUpdated = false;
        ButtonClose.buttonInteractable = true;
        ButtonClose.buttonText = "Close";
    }

    public void MouseClick()
    {
        OnMouseDown();
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
