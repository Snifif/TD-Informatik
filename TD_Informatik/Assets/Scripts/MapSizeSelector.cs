using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSizeSelector : MonoBehaviour
{
    private bool heightChanged;
    private bool widthChanged;
    public static int mapHeight = 9;
    public static int mapWidth = 9;
    void Start()
    {
        heightChanged = false;
        widthChanged = false;
        mapHeight = 9;
        mapWidth = 9;
    }

    
    public void buttonStartPressedScript()
    {
        if(heightChanged == true && widthChanged == true)
        {
            
            SceneManager.LoadScene("SampleScene");
        }
        
    }
    public void buttonBackPressedScript()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void mapHeightChanged(string mapHeightString)
    {
        Debug.Log("mapHeight: " + mapHeightString);
        try
        {
            mapHeight = int.Parse(mapHeightString);
            heightChanged = true;
            if (mapHeight < 5 || mapHeight > 100)
            {
                heightChanged = false;
            }
            Debug.Log("mapHeight");
        }
        catch
        {
            heightChanged = false;
        }
    }
    public void mapWidthChanged(string mapWidthString)
    {
        Debug.Log("mapWidth: " + mapWidthString);
        try
        {
            mapWidth = int.Parse(mapWidthString);
            widthChanged = true;
            if (mapWidth < 5 || mapWidth > 100)
            {
                widthChanged = false;
            }
            Debug.Log("mapWidth");
        }
        catch
        {
            widthChanged = false;
        }
    }
}
