using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class EnemyBehavior2 : MonoBehaviour
{
    private GameObject TargetNode;
<<<<<<< HEAD
    [SerializeField] private float Health;
    [SerializeField] private int Damage;
    [SerializeField] private float Speed;
    [SerializeField] private int KillReward;
=======
    [SerializeField] private float health = 1;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float killReward;
>>>>>>> newBranchTest


    private float distanceX;
    private float distanceZ;

<<<<<<< HEAD
    public Vector3 TargetPos;
    public Vector3 EndNodePos;

    public GameObject Enemy;
=======
    public Vector3 targetPos; //hat x,y,z
    public Vector3 EndNodePos;

    private void Awake()
    {
        ListEnemies.enemies.Add(gameObject);
    }
>>>>>>> newBranchTest

    void Start()
    {
        InitializeEnemy();

    }

    void Update()
    {
        CheckTargetNode();
        CheckEnemy();
        MoveEnemy();
    }

    private void InitializeEnemy()
    {
<<<<<<< HEAD
        TargetNode = GenerateMap.PathNodes[0];
        GameObject newEnemy = Instantiate(Enemy);
        gameObject.GetComponent<HQ>().EnemyList.Add(Enemy);
=======
        TargetNode = GenerateMap.startTile;
    }
>>>>>>> newBranchTest

    }
    private void MoveEnemy()
    {

<<<<<<< HEAD
      TargetPos = new Vector3(TargetNode.transform.position.x, 1f, TargetNode.transform.position.z ); // nur in x und z Richtung zum TargetNode bewegen
      transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime   ); // deltaTime, damit bei schnell hintereinander void update Aufrufen der Gegner nicht super schnell ist
=======
      targetPos = new Vector3(TargetNode.transform.position.x, 1f, TargetNode.transform.position.z ); // nur in x und z Richtung zum TargetNode bewegen
      transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime   );
>>>>>>> newBranchTest
     

    }

    private void CheckTargetNode()
    {
        distanceX = Mathf.Abs(transform.position.x - TargetNode.transform.position.x);
        distanceZ = Mathf.Abs(transform.position.z - TargetNode.transform.position.z);

        if ( TargetNode != null && TargetNode != GenerateMap.endTile )
        {
            
            if (distanceX <= 0.01f && distanceZ <= 0.01f)
            {
                int NextTargetNode = GenerateMap.PathNodes.IndexOf(TargetNode) + 1;         // Nexte TargetNode
                TargetNode = GenerateMap.PathNodes[NextTargetNode];
<<<<<<< HEAD

=======
>>>>>>> newBranchTest
            }

        }

        if (TargetNode.transform.position == GenerateMap.endTile.transform.position)
        {
            EndNodePos = new Vector3(TargetNode.transform.position.x, 1, TargetNode.transform.position.z);
        }
        if (transform.position == EndNodePos)           // wenn am Ziel -> Tod des Enemy und schaden bekommen
        {
            
            gameObject.GetComponent<HQ>().HQHealth = gameObject.GetComponent<HQ>().HQHealth - Damage;           // Lebenspunkte schaden abziehen
            die();
        }

    }

    private void CheckEnemy()
    {
        if (health <= 0f)
        {
           
            gameObject.GetComponent<HQ>().Money = gameObject.GetComponent<HQ>().Money + KillReward;       // füge Killreward zu Geld hinzu
            die();
        }

    }

    public void takeDamage (float damageAmount)
    {
        health = health - damageAmount;

        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
<<<<<<< HEAD
        gameObject.GetComponent<HQ>().EnemyList.Remove(Enemy);
=======
        ListEnemies.enemies.Remove(gameObject);
>>>>>>> newBranchTest
        Destroy(transform.gameObject);
        

    }
}
