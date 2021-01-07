using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class EnemyBehavior2 : MonoBehaviour
{
    private GameObject TargetNode;
    [SerializeField]
    private float Health;
    [SerializeField]
    private float Damage;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float KillReward;


    private float Distancex;
    private float Distancez;

    public Vector3 TargetZPos;
    public Vector3 TargetXPos;
    public Vector3 EndNodePos;

    void Start()
    {
        InitializeEnemy();
    } 

    // Update is called once per frame
    void Update()
    {
        CheckTargetNode();
        CheckEnemy();
        MoveEnemy();
    }

    private void InitializeEnemy()
    {
        TargetNode = GenerateMap.PathNodes[0];
    }

    private void MoveEnemy()
    {

      TargetXPos = new Vector3(TargetNode.transform.position.x, 1f, TargetNode.transform.position.z ); // nur in x und z Richtung zum TargetNode bewegen
      transform.position = Vector3.MoveTowards(transform.position, TargetXPos, Speed * Time.deltaTime   );
     

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
            // Lebenspunkte schaden abziehen
        }

    }

    private void CheckEnemy()
    {
        if (Health <= 0f)
        {
            die();
            // füge Killreward zu Geld hinzu

        }

    }
    private void die()
    {
        Destroy(transform.gameObject);
    }
}
