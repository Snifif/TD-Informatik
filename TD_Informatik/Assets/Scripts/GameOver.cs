using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleScene : MonoBehaviour
{

    public static void StartPressedScriptOver()
    {
        SceneManager.LoadScene("Overscreen"); 
    }

    public void QuitScript()
    {
        Application.Quit();
    }
}