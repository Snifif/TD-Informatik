using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class EnemyBehavior2 : MonoBehaviour
{
    private GameObject TargetNode;
    [SerializeField] private float health = 1;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float killReward;


    private float distanceX;
    private float distanceZ;

    public Vector3 targetPos; //hat x,y,z
    public Vector3 EndNodePos;

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
    }

    private void MoveEnemy()
    {

        targetPos = new Vector3(TargetNode.transform.position.x, 1f, TargetNode.transform.position.z); // nur in x und z Richtung zum TargetNode bewegen
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);


    }

    private void CheckTargetNode()
    {
        distanceX = Mathf.Abs(transform.position.x - TargetNode.transform.position.x);
        distanceZ = Mathf.Abs(transform.position.z - TargetNode.transform.position.z);

        if (TargetNode != null && TargetNode != GenerateMap.endTile)
        {

            if (distanceX <= 0.01f && distanceZ <= 0.01f)
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
        if (health <= 0f)
        {
            die();
            // füge Killreward zu Geld hinzu

        }

    }

    public void takeDamage(float damageAmount)
    {
        health = health - damageAmount;

        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
        ListEnemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }
}
