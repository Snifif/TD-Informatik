using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overscreen : MonoBehaviour
{

    public void StartPressedScriptMain()
    {
        SceneManager.LoadScene("MainMenu");     // Ladet SampleScene 
    }

    public void QuitScript()
    {
        Application.Quit();
    }
}