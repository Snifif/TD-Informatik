using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisableScript : MonoBehaviour
{
    Button button;
    Button button2;
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = true;
        // button2 = GameObject.Find("Pause").GetComponent<Button>();
    }

    private void disableButton()
    {
        button.interactable = false;
        Debug.Log("Disabled Button");
    }
    private void enableButton()
    {
        button.interactable = true;
        Debug.Log("Enabled Button");
    }
    public void onButtonClick()
    {
        if (button.interactable)
        {
            disableButton();
        }
        else
        {
            enableButton();
        }
    }
    void Update()
    {
        
    }
}
