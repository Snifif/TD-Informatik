using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public static bool TT = false;
    public static bool LT = false;
    bool Paused = false;

    public void TurretT()
    {
       TT = true;
       LT = false;
    }

    public void LaserT()
    {

       TT = false;
       LT = true;
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
        SceneManager.LoadScene("MainMenu");
    }
}
