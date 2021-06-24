using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTowerInfo : MonoBehaviour
{
    Button button;
    public static bool buttonUpdated = true;
    public static bool buttonInteractable = false;
    public static string buttonText;
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
        buttonUpdated = true;
        buttonInteractable = false;
        buttonText = "";
    }

    private void updateButton()
    {
        button.interactable = buttonInteractable;
        buttonUpdated = true;

    }
    void Update()
    {
        if (buttonUpdated != true)
        {
            updateButton();
        }
    }
}
