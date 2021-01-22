using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update
    public int deathcount = 8; //Killcount
    public int money; //Startgeld noch festlegen
    public Text MoneyShow;

    void Start()
    {
        MoneyShow = GetComponent<Text>();

    }

    void Update()
    {
        for (deathcount = EnemyBehavior2.deathtrack; deathcount > 0; deathcount--)
        {
            money = money + 5;
            MoneyShow.text = "Money: " + money.ToString();
        }
        EnemyBehavior2.deathtrack = 0;
    }
}
