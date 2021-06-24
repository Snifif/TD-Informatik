using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonTowerInfo : MonoBehaviour
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
    void Update()
    {
        if (buttonUpdated != true)
        {
            updateButton();
        }
    }
}
