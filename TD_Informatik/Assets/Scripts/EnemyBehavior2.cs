using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class EnemyBehavior2 : MonoBehaviour
{
    private GameObject TargetNode;
    [SerializeField] private float health = 1;
    [SerializeField] private float damage;
    public static float speed = 1;
    [SerializeField] private float killReward;


    private float distanceX;
    private float distanceZ;

    public Vector3 targetPos; //hat x,y,z
    public Vector3 EndNodePos;
    public static int deathtrack; //Geld zählt bei Kills hoch
    public static int playerdmg;
    [SerializeField] internal int indexOfTargetNode;

    private void Awake()
    {
        ListEnemies.enemies.Add(gameObject);
    }

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
        TargetNode = GenerateMap.startTile;
        indexOfTargetNode = GenerateMap.PathNodes.IndexOf(TargetNode);
    }

    private void MoveEnemy()
    {

        targetPos = new Vector3(TargetNode.transform.position.x, 1f, TargetNode.transform.position.z); // nur in x und z Richtung zum TargetNode bewegen
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);


    }

    public void CheckTargetNode() // chekt distanz und wechselt ziel wenn in distnaz zu klein ist
    {
        distanceX = Mathf.Abs(transform.position.x - TargetNode.transform.position.x);
        distanceZ = Mathf.Abs(transform.position.z - TargetNode.transform.position.z);

        if (TargetNode != null && TargetNode != GenerateMap.endTile)    
        {

            if (distanceX <= 0.01f && distanceZ <= 0.01f)
            {
                int NextTargetNode = GenerateMap.PathNodes.IndexOf(TargetNode) + 1;
                TargetNode = GenerateMap.PathNodes[NextTargetNode];
                indexOfTargetNode = NextTargetNode;
            }

        }

        if (TargetNode.transform.position == GenerateMap.endTile.transform.position)
        {
            EndNodePos = new Vector3(TargetNode.transform.position.x, 1, TargetNode.transform.position.z);
        }
        if (transform.position == EndNodePos)  // wenn am Ziel -> Tod des Enemy und schaden bekommen
        {
            playerdmg = playerdmg + 1; // schden kriegen
            die();   
        }

    }

    public void getIndex()
    {
        indexOfTargetNode = GenerateMap.PathNodes.IndexOf(TargetNode);
        int indexTest = indexOfTargetNode;
    }

    private void CheckEnemy()
    {
        if (health <= 0f)
        {
            die();

        }

    }

    public void takeDamage(float damageAmount)
    {
        health = health - damageAmount;

        if (health <= 0)
        {
            die();
            deathtrack = deathtrack + 1; //Trackt nur ingesamt kills
        }
    }
    

    private void die()
    {
        ListEnemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }
}