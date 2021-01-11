using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class EnemyBehavior2 : MonoBehaviour
{
    private GameObject TargetNode;
    [SerializeField] private float Health;
    [SerializeField] private int Damage;
    [SerializeField] private float Speed;
    [SerializeField] private int KillReward;


    private float Distancex;
    private float Distancez;

    public Vector3 TargetPos;
    public Vector3 EndNodePos;

    public GameObject Enemy;

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
        TargetNode = GenerateMap.PathNodes[0];
        GameObject newEnemy = Instantiate(Enemy);
        gameObject.GetComponent<HQ>().EnemyList.Add(Enemy);

    }
    private void MoveEnemy()
    {

      TargetPos = new Vector3(TargetNode.transform.position.x, 1f, TargetNode.transform.position.z ); // nur in x und z Richtung zum TargetNode bewegen
      transform.position = Vector3.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime   ); // deltaTime, damit bei schnell hintereinander void update Aufrufen der Gegner nicht super schnell ist
     

    }

    private void CheckTargetNode()
    {
        Distancex = Mathf.Abs(transform.position.x - TargetNode.transform.position.x);
        Distancez = Mathf.Abs(transform.position.z - TargetNode.transform.position.z);

        if ( TargetNode != null && TargetNode != GenerateMap.endTile )
        {
            
            if (Distancex <= 0.01f && Distancez <= 0.01f)
            {
                int NextTargetNode = GenerateMap.PathNodes.IndexOf(TargetNode) + 1;
                TargetNode = GenerateMap.PathNodes[NextTargetNode];


            }

        }

        if (TargetNode.transform.position == GenerateMap.endTile.transform.position)
        {
            EndNodePos = new Vector3(TargetNode.transform.position.x, 1, TargetNode.transform.position.z);
        }
        if (transform.position == EndNodePos)  // wenn am Ziel -> Tod des Enemy und schaden bekommen
        {
            die();
            gameObject.GetComponent<HQ>().HQHealth = gameObject.GetComponent<HQ>().HQHealth - Damage;
            // Lebenspunkte schaden abziehen
        }

    }

    private void CheckEnemy()
    {
        if (Health <= 0f)
        {
            die();
            gameObject.GetComponent<HQ>().Money = gameObject.GetComponent<HQ>().Money + KillReward;       // füge Killreward zu Geld hinzu

        }

    }
    private void die()
    {
        Destroy(transform.gameObject);
        gameObject.GetComponent<HQ>().EnemyList.Remove();

    }
}
