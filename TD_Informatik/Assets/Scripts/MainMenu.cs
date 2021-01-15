using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartPressedScript()
    {
        SceneManager.LoadScene("SampleScene");     // Ladet SampleScene 
    }

    public void QuitScript()
    {
        Application.Quit();
    }
}
