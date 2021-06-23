using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public static bool TT = false;
    public static bool LT = false;
    public static bool YT = false;
    bool Paused = false;
    
    public void TurretT()
    {
       TT = true;
       LT = false;
       YT = false;
    }

    public void LaserT()
    {

       TT = false;
       LT = true;
       YT = false;
    }

    public void Yeeter()
    {
       TT = false;
       LT = false;
       YT = true;
    }
    public void Pause()
    { 
        if(Paused == false)
        {
            Time.timeScale = 0;
            Paused = true;
        }
        else
        {
            Time.timeScale = 1;
            Paused = false;
        }
    }

    public void MAINMENU()
    {
        Time.timeScale = 1;
        Paused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
