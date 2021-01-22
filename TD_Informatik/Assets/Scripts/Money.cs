using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update
    public int deathcount; //Killcount
    public static int money; //Startgeld noch festlegen (50 Start, 20T/100L?) //70 / 20 / 200 //
    public static Text MoneyShow;

    void Start()
    {
        MoneyShow = GetComponent<Text>();
        money = 70;
        MoneyShow.text = "Money: " + money.ToString();
    }

    void Update()
    {

        for (deathcount = EnemyBehavior2.deathtrack; deathcount > 0; deathcount--)
        {
            money = money + 5;
        }
        EnemyBehavior2.deathtrack = 0;
        UpdateBalance();
    }
    public static void UpdateBalance()
    {
        MoneyShow.text = "Money: " + money.ToString();
    }
}