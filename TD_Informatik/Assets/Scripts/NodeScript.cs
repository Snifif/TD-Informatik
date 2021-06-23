using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public GameObject LaserTower;
    public GameObject TurretTower;
    bool towerPlaced = false; // eventuell unnötig
    private GameObject ConnectedTurret;
    private void Start()
    {
        towerPlaced = false;
    }

    void Update()
    {

    }

    private void OnMouseDown()  // Spawnen vom gewünschten Tower, wenn auf ein Node gedrückt wird
    {
        if(ConnectedTurret==null){     
            if (GameMenu.LT == true && Money.money >= 140)   
            {
                ConnectedTurret = Instantiate(LaserTower, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
                Money.money = Money.money - 140;
                towerPlaced = true;
            }

            if (GameMenu.TT == true && Money.money >=20)
            {
                ConnectedTurret = Instantiate(TurretTower, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
                Money.money = Money.money - 20;
                towerPlaced = true;
            }
            Money.UpdateBalance();
            
        }
    }
}

