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

    private void OnMouseDown()
    {
        {
            if (Input.GetMouseButton(0))
            {
                if (GameMenu.LT == true)
                {
                    Instantiate(LaserTower, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);
                }

                if (GameMenu.TT == true)
                {
                    Instantiate(TurretTower, new Vector3(gameObject.transform.position.x, 0.8f, gameObject.transform.position.z), Quaternion.identity);

                }
            }
        }
    }
}

