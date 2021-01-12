using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private float Range;
    private int Damage;
    private float DPS;          // Schaden pro Sekunde


    public List<GameObject> Towers = new List<GameObject>();

    public GameObject Towerr;

    void Start()
    {
        
    }

    private void InitiliazeEnemy()
    {
        GameObject newTOwer = Instantiate(Towerr);
        Towers.Add(newTOwer);

    }

    private void CheckNearestEnemy()
    {

    }
    void Update()
    {
        
    }
}
