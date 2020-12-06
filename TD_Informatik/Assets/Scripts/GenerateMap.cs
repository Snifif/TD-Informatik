using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject Node;
    public GameObject NodePath;

    [SerializeField] private int MapBREITE = 9;
    [SerializeField] private int MapHoehe = 9;

    private List<GameObject> MapNodes = new List<GameObject>();
    private List<GameObject> EnemyNodes = new List<GameObject>();

    private bool reachedX = false; // temporär
    private bool reachedZ = false; //temporär
    private GameObject currentTile; // temporär
    private int currentIndex; // temporär
    private int nextIndex; // temporär
    [SerializeField] private int counter = 0; // temporär
    [SerializeField] private int counter2 = 0; //temporär


    private void Start()
    {
        MapGenerate();
    }

    private List<GameObject> getTopTiles() // erstellt die Liste topTiles und fügt alle Tiles am oberen Rand in die Liste von links nach rechts ein, x=[(MapHoehe-1)*2], z={0;2;4;...;(MapBREITE-2)*2;(MapBREITE-1)*2}
    {
        List<GameObject> edgeTiles = new List<GameObject>();
        for (int i = MapBREITE * (MapHoehe - 1); i < MapBREITE * MapHoehe; i++)
        {
            edgeTiles.Add(MapNodes[i]);
        }
        return edgeTiles;
    }

    private List<GameObject> getBottomTiles() // erstellt die Liste bottomTiles und fügt alle Tiles am unteren Rand in die Liste von links nach rechts ein, x=0, z={0;2;4;...;(MapBREITE-2)*2;(MapBREITE-1)*2}
    {
        List<GameObject> edgeTiles = new List<GameObject>();
        for (int i = 0; i < MapBREITE; i++)
        {
            edgeTiles.Add(MapNodes[i]);
        }
        return edgeTiles;
    }

    private void MapGenerate()
    {
        for (int z = 0; z < 2 * MapBREITE; z = z + 2)
        {
            for (int x = 0; x < 2 * MapHoehe; x = x + 2)
            {
                GameObject newNode = Instantiate(Node);
                newNode.transform.position = new Vector3(x, 0, z);
                MapNodes.Add(newNode);

            }
        }

        List<GameObject> topTiles = getTopTiles();
        List<GameObject> bottomTiles = getBottomTiles();

        GameObject startTile;
        GameObject endTile;

        int randTop = Random.Range(0, MapBREITE);
        int randBottom = Random.Range(0, MapBREITE);

        startTile = topTiles[randTop]; //setzt zufälliges Tile vom oberen Rand als Startpunkt fest
        endTile = bottomTiles[randBottom]; //setzt zufälliges Tile vom unteren Rand als Startpunkt fest

        currentTile = startTile;
        counter = 0;
        moveDown();
        while (reachedX == false)
        {
            counter++;
            
            if(currentTile.transform.position.x > endTile.transform.position.x)
            {
                moveLeft();
            }
            else if (currentTile.transform.position.x < endTile.transform.position.x)
            {
                moveRight();
            }
            else
            {
                reachedX = true;
            }
        }
        counter2 = 0;
        while (reachedZ == false)
        {
            counter2++;
            
            if(currentTile.transform.position.z > endTile.transform.position.z)
            {
                moveDown();
            }
            else
            {
                reachedZ = true;
            }
        }

        EnemyNodes.Add(endTile);

        foreach(GameObject obj in EnemyNodes)
        {
            
            Destroy(obj);
            GameObject newPathNode = Instantiate(NodePath);
            newPathNode.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            
        }

    }
    
    private void moveDown() //temporär
    {
        EnemyNodes.Add(currentTile);
        currentIndex = MapNodes.IndexOf(currentTile);
        nextIndex = currentIndex - MapHoehe;
        currentTile = MapNodes[nextIndex];
    }
    
    private void moveLeft() //temporär
    {
        EnemyNodes.Add(currentTile);
        currentIndex = MapNodes.IndexOf(currentTile);
        nextIndex = currentIndex-1;
        currentTile = MapNodes[nextIndex];
    }
    private void moveRight() //temporär
    {
        EnemyNodes.Add(currentTile);
        currentIndex = MapNodes.IndexOf(currentTile);
        nextIndex = currentIndex+1;
        currentTile = MapNodes[nextIndex];
    }

}
