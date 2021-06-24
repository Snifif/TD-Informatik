using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAttackSpeed : MonoBehaviour
{
    Button button;
    private Text text;
    public static bool buttonUpdated = true;
    public static bool buttonInteractable = false;
    public static string buttonText;
    public static GameObject tower = null;
    void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        button.interactable = false;
        buttonUpdated = true;
        buttonInteractable = false;
        buttonText = "";
        text.text = buttonText;
    }

    private void updateButton()
    {
        button.interactable = buttonInteractable;
        buttonUpdated = true;
        text.text = buttonText;
    }
    public void buttonClick()
    {
        TowerBasic towerScript = tower.GetComponent<TowerBasic>();
        int upgradePrice = towerScript.turretPrice / 2 * (int)Mathf.Pow(2, towerScript.attackSpeedMultiplier);
        if (Money.money >= upgradePrice)
        {
            towerScript.attackSpeedMultiplier++;
            Money.money = Money.money - upgradePrice;
            towerScript.turretValue = towerScript.turretValue + upgradePrice;
            towerScript.attackSpeed = towerScript.baseAttackSpeed / towerScript.attackSpeedMultiplier;
        }
        towerScript.MouseClick();
    }
    void Update()
    {
        if (buttonUpdated != true)
        {
            updateButton();
        }
    }
}
