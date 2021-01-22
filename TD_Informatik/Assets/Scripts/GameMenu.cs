using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{

    public static bool TT = false;
    public static bool LT = false;

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
        // Quit und Continue Knopf Anzeigen
    }
}
