using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSell : MonoBehaviour
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
        Money.money = Money.money + (towerScript.turretValue)/2;
        Destroy(tower.transform.gameObject);
        buttonInteractable = false;
        buttonUpdated = false;
        ButtonTowerInfo.buttonUpdated = false;
        ButtonTowerInfo.buttonInteractable = false;
        ButtonDamage.buttonUpdated = false;
        ButtonDamage.buttonInteractable = false;
        ButtonAttackSpeed.buttonUpdated = false;
        ButtonAttackSpeed.buttonInteractable = false;
        ButtonAttackRange.buttonUpdated = false;
        ButtonAttackRange.buttonInteractable = false;
        ButtonClose.buttonUpdated = false;
        ButtonClose.buttonInteractable = false;
        buttonText = "";
        ButtonTowerInfo.buttonText = "";
        ButtonDamage.buttonText = "";
        ButtonAttackSpeed.buttonText = "";
        ButtonAttackRange.buttonText = "";
        ButtonClose.buttonText = "";
    }
    void Update()
    {
        if (buttonUpdated != true)
        {
            updateButton();
        }
        
    }
}
