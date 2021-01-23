using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public GameObject LaserTower;
    public GameObject TurretTower;

    void Update()
    {

    }

    private void OnMouseDown()  // Spawnen vom gewünschten Tower, wenn auf ein Node gedrückt wird
    {
        {     
            if (GameMenu.LT == true && Money.money >= 130)   
            {
                Instantiate(LaserTower, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
                Money.money = Money.money - 130;
            }

            if (GameMenu.TT == true && Money.money >=20)
            {
                Instantiate(TurretTower, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
                Money.money = Money.money - 20;
            }
            Money.UpdateBalance();
            
        }
    }
}

