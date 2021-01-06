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

    void Start()
    {
        InitializeEnemy();
    } 

    // Update is called once per frame
    void Update()
    {
        CheckTargetNode();
        MoveEnemy();
    }

    private void InitializeEnemy()
    {
        TargetNode = GenerateMap.PathNodes[0];
    }

    private void MoveEnemy()
    {
        
        if( Distancez >= 0.01)  //in z bewegen
        {
            TargetZPos = new Vector3(transform.position.x, transform.position.y + 0.3f, TargetNode.transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, TargetZPos , Speed * Time.deltaTime);
        }
        transform.position = Vector3.MoveTowards(transform.position, TargetNode.transform.position, Speed * Time.deltaTime);


        if (Distancex >= 0.01)  //in x bewegen
        {
            TargetXPos = new Vector3(TargetNode.transform.position.x, transform.position.y + 0.3f, transform.position.z );

            transform.position = Vector3.MoveTowards(transform.position, TargetXPos, Speed * Time.deltaTime);
        }

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
        if (transform.position == GenerateMap.endTile.transform.position)
        {
            die();
        }

    }

    private void die()
    {
        Destroy(transform.gameObject);
    }
}
