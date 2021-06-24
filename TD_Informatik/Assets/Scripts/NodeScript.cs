using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public GameObject LaserTower;
    public GameObject TurretTower;
    public GameObject Yeeter;
    private int laserTowerPrice = 160;
    private int turretTowerPrice = 20;
    private int yeeterPrice = 200;

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
            if (GameMenu.LT == true && Money.money >= 160)   
            {
                ConnectedTurret = Instantiate(LaserTower, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
                Money.money = Money.money - 160;
                TowerBasic tower = ConnectedTurret.GetComponent<TowerBasic>();
                tower.turretType = "LaserTower";
                tower.turretPrice = laserTowerPrice;
                tower.turretValue = laserTowerPrice;
                towerPlaced = true;
            }

            if (GameMenu.TT == true && Money.money >=20)
            {
                ConnectedTurret = Instantiate(TurretTower, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
                Money.money = Money.money - 20;
                TowerBasic tower = ConnectedTurret.GetComponent<TowerBasic>();
                tower.turretType = "TurretTower";
                tower.turretPrice = turretTowerPrice;
                tower.turretValue = turretTowerPrice;
                towerPlaced = true;
            }

            if (GameMenu.YT == true && Money.money >= 200)
            {
                ConnectedTurret = Instantiate(Yeeter, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
                Money.money = Money.money - 200;
                TowerBasic tower = ConnectedTurret.GetComponent<TowerBasic>();
                tower.turretType = "Yeeter";
                tower.turretPrice = yeeterPrice;
                tower.turretValue = yeeterPrice;
                towerPlaced = true;
            }
            Money.UpdateBalance();

            
        }
        if(ConnectedTurret!=null)
        {
            TowerBasic tower = ConnectedTurret.GetComponent<TowerBasic>();
            tower.MouseClick();
        }
    }
}

