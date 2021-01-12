using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject Node;
    public GameObject NodePath;

    [SerializeField] private int MapBREITE = 9;
    [SerializeField] private int MapHoehe = 9;

    public static List<GameObject> MapNodes = new List<GameObject>();
    private List<GameObject> EnemyNodes = new List<GameObject>();
    public static List<GameObject> PathNodes = new List<GameObject>();

    public static GameObject startTile;
    public static GameObject endTile;

    private int randomStorage;
    private List<int> blockedIndex = new List<int>();
    private bool finishedMapGen = false;
    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;
    private bool moved = false;
    [SerializeField] private int endTilePos;

    public Material StartMaterial;
    public Material EndMaterial;

    [SerializeField] private int lengthCounterPath;

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

        GameObject startTileGen;
        GameObject endTileGen;

        int randTop = Random.Range(0, MapBREITE);
        int randBottom = Random.Range(0, MapBREITE);

        

        startTileGen = topTiles[randTop]; //setzt zufälliges Tile vom oberen Rand als Startpunkt fest
        endTileGen = bottomTiles[randBottom]; //setzt zufälliges Tile vom unteren Rand als Startpunkt fest
        endTilePos = randBottom;

        currentTile = startTileGen;

        for (int i = 0; i < MapBREITE*MapHoehe; i++) // fügt, abhängig von der Größe der Map, der Liste blockedIndex pro Maplement ein Element mit dem Wert 0 hinzu
        {
            blockedIndex.Add(0);
        }

        //Path Generation
        for (int i = 0; i < MapBREITE; i++) // blockiert alle Elemente der oberen Reihe, die nicht das endTile sind
        {
            if (randTop != i)
            {
                blockedIndex[(MapHoehe - 1) * MapBREITE + i] = 1;
            }
        }
        for (int i = 0; i < MapBREITE; i++) // blockiert alle Elemente der unteren Reihe, die nicht das startTile sind
        {
            if (randBottom!=i)
            {
                blockedIndex[i] = 1;
            }
        }
        moved = true;
        while (finishedMapGen == false)
        {
            if (moved == true) // key part
            {
                EnemyNodes.Add(currentTile);
                currentIndex = MapNodes.IndexOf(currentTile);
                moved = false;
            }
            if (currentIndex > ((MapHoehe - 1) * MapBREITE)-1) //Überprüfung, ob es am oberen Rand ist
            {
                moveDown();
            }
            else if (currentIndex < MapBREITE) //Überprüfung, ob es am unteren Rand ist
            {
                finishedMapGen = true;
            }
            else if((blockedIndex[currentIndex - MapHoehe]) == 0 && (blockedIndex[currentIndex + MapHoehe]) == 0 && (blockedIndex[currentIndex - 1]) == 0 && (blockedIndex[currentIndex + 1]) == 0) //Bewegung in alle Richtungen möglich
            {
                randomStorage = Random.Range(0, 4);
                if (currentIndex % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    if (randomStorage == 0)
                    {
                        moveDown();
                    }
                    if (randomStorage == 1)
                    {
                        moveUp();
                    }
                    if (randomStorage == 2)
                    {
                        moveRight();
                    }
                    if (randomStorage == 3)
                    {
                        moveRight();
                    }
                }
                else if ((currentIndex + 1) % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    if (randomStorage == 0)
                    {
                        moveDown();
                    }
                    if (randomStorage == 1)
                    {
                        moveUp();
                    }
                    if (randomStorage == 2)
                    {
                        moveLeft();
                    }
                    if (randomStorage == 3)
                    {
                        moveLeft();
                    }
                }
                else
                {
                    if (randomStorage == 0)
                    {
                        moveDown();
                    }
                    if(randomStorage == 1)
                    {
                        moveUp();
                    }
                    if(randomStorage == 2)
                    {
                        moveLeft();
                    }
                    if(randomStorage == 3)
                    {
                        moveRight();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 0 && (blockedIndex[currentIndex + MapHoehe]) == 0 && (blockedIndex[currentIndex - 1]) == 0 && (blockedIndex[currentIndex + 1]) == 1) //Bewegung nach rechts nicht möglich
            {
                randomStorage = Random.Range(0, 3);
                if (currentIndex % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    if (randomStorage == 0)
                    {
                        moveDown();
                    }
                    if (randomStorage == 1)
                    {
                        moveUp();
                    }
                    if (randomStorage == 2)
                    {
                        moveDown();
                    }
                }
                else
                {
                    if (randomStorage == 0)
                    {
                        moveDown();
                    }
                    if (randomStorage == 1)
                    {
                        moveUp();
                    }
                    if (randomStorage == 2)
                    {
                        moveLeft();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 0 && (blockedIndex[currentIndex + MapHoehe]) == 0 && (blockedIndex[currentIndex - 1]) == 1 && (blockedIndex[currentIndex + 1]) == 0) //Bewegung nach links nicht möglich
            {
                randomStorage = Random.Range(0, 3);
                if ((currentIndex + 1) % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    if (randomStorage == 0)
                    {
                        moveDown();
                    }
                    if (randomStorage == 1)
                    {
                        moveUp();
                    }
                    if (randomStorage == 2)
                    {
                        moveDown();
                    }
                }
                else
                {
                    if(randomStorage == 0)
                    {
                        moveDown();
                    }
                    if(randomStorage == 1)
                    {
                        moveUp();
                    }
                    if(randomStorage == 2)
                    {
                        moveRight();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 0 && (blockedIndex[currentIndex + MapHoehe]) == 0 && (blockedIndex[currentIndex - 1]) == 1 && (blockedIndex[currentIndex + 1]) == 1) //Bewegung nach links und rechts nicht möglich
            {
                randomStorage = Random.Range(0, 2);
                if(randomStorage == 0)
                {
                    moveDown();
                }
                if(randomStorage == 1)
                {
                    moveUp();
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 0 && (blockedIndex[currentIndex + MapHoehe]) == 1 && (blockedIndex[currentIndex - 1]) == 0 && (blockedIndex[currentIndex + 1]) == 0) //Bewegung nach oben nicht möglich
            {
                randomStorage = Random.Range(0, 3);
                if (currentIndex % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    if (randomStorage == 0)
                    {
                        moveDown();
                    }
                    if (randomStorage == 1)
                    {
                        moveRight();
                    }
                    if (randomStorage == 2)
                    {
                        moveRight();
                    }
                }
                else if ((currentIndex + 1) % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    if (randomStorage == 0)
                    {
                        moveDown();
                    }
                    if (randomStorage == 1)
                    {
                        moveLeft();
                    }
                    if (randomStorage == 2)
                    {
                        moveLeft();
                    }
                }
                else
                {
                    if(randomStorage == 0)
                    {
                        moveDown();
                    }
                    if(randomStorage == 1)
                    {
                        moveLeft();
                    }
                    if(randomStorage == 2)
                    {
                        moveRight();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 0 && (blockedIndex[currentIndex + MapHoehe]) == 1 && (blockedIndex[currentIndex - 1]) == 0 && (blockedIndex[currentIndex + 1]) == 1) //Bewegung nach oben und rechts nicht möglich
            {
                randomStorage = Random.Range(0, 2);
                if (currentIndex % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    moveDown();
                }
                else
                {
                    if(randomStorage == 0)
                    {
                        moveDown();
                    }
                    if(randomStorage == 1)
                    {
                        moveLeft();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 0 && (blockedIndex[currentIndex + MapHoehe]) == 1 && (blockedIndex[currentIndex - 1]) == 1 && (blockedIndex[currentIndex + 1]) == 0) //Bewegung nach oben und links nicht möglich
            {
                randomStorage = Random.Range(0, 2);
                if ((currentIndex + 1) % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    moveDown();
                }
                else
                {
                    if(randomStorage == 0)
                    {
                        moveDown();
                    }
                    if(randomStorage == 1)
                    {
                        moveRight();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 0 && (blockedIndex[currentIndex + MapHoehe]) == 1 && (blockedIndex[currentIndex - 1]) == 1 && (blockedIndex[currentIndex + 1]) == 1) //Bewegung nach oben, links und rechts nicht möglich
            {
                moveDown();
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 1 && (blockedIndex[currentIndex + MapHoehe]) == 0 && (blockedIndex[currentIndex - 1]) == 0 && (blockedIndex[currentIndex + 1]) == 0) //Bewegung nach unten nicht möglich
            {
                randomStorage = Random.Range(0, 3);
                if (currentIndex % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    if (randomStorage == 0)
                    {
                        moveUp();
                    }
                    if (randomStorage == 1)
                    {
                        moveRight();
                    }
                    if (randomStorage == 2)
                    {
                        moveRight();
                    }
                }
                else if ((currentIndex + 1) % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    if (randomStorage == 0)
                    {
                        moveUp();
                    }
                    if (randomStorage == 1)
                    {
                        moveLeft();
                    }
                    if (randomStorage == 2)
                    {
                        moveLeft();
                    }
                }
                else
                {
                    if(randomStorage == 0)
                    {
                        moveUp();
                    }
                    if(randomStorage == 1)
                    {
                        moveLeft();
                    }
                    if(randomStorage == 2)
                    {
                        moveRight();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 1 && (blockedIndex[currentIndex + MapHoehe]) == 0 && (blockedIndex[currentIndex - 1]) == 0 && (blockedIndex[currentIndex + 1]) == 1) //Bewegung nach unten und rechts nicht möglich
            {
                randomStorage = Random.Range(0, 2);
                if (currentIndex % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    moveUp();
                }
                else
                {
                    if(randomStorage == 0)
                    {
                        moveUp();
                    }
                    if(randomStorage == 1)
                    {
                        moveLeft();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 1 && (blockedIndex[currentIndex + MapHoehe]) == 0 && (blockedIndex[currentIndex - 1]) == 1 && (blockedIndex[currentIndex + 1]) == 0) //Bewegung nach unten und links nicht möglich
            {
                randomStorage = Random.Range(0, 2);
                if ((currentIndex + 1) % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    moveUp();
                }
                else
                {
                    if(randomStorage == 0)
                    {
                        moveUp();
                    }
                    if(randomStorage == 1)
                    {
                        moveRight();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 1 && (blockedIndex[currentIndex + MapHoehe]) == 0 && (blockedIndex[currentIndex - 1]) == 1 && (blockedIndex[currentIndex + 1]) == 1) //Bewegung nach unten, links und rechts nicht möglich
            {
                moveUp();
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 1 && (blockedIndex[currentIndex + MapHoehe]) == 1 && (blockedIndex[currentIndex - 1]) == 0 && (blockedIndex[currentIndex + 1]) == 0) //Bewegung nach unten und oben nicht möglich
            {
                randomStorage = Random.Range(0, 2);
                if (currentIndex % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    moveRight();
                }
                else if ((currentIndex + 1) % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    moveLeft();
                }
                else
                {
                    if(randomStorage == 0)
                    {
                        moveLeft();
                    }
                    if(randomStorage == 1)
                    {
                        moveRight();
                    }
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 1 && (blockedIndex[currentIndex + MapHoehe]) == 1 && (blockedIndex[currentIndex - 1]) == 0 && (blockedIndex[currentIndex + 1]) == 1) //Bewegung nach unten, oben und rechts nicht möglich
            {
                if (currentIndex % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    finishedMapGen = true;
                }
                else
                {
                    moveLeft();
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 1 && (blockedIndex[currentIndex + MapHoehe]) == 1 && (blockedIndex[currentIndex - 1]) == 1 && (blockedIndex[currentIndex + 1]) == 0) // Bewegung nach unten, oben und links nicht möglich
            {
                if ((currentIndex + 1) % MapBREITE == 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    finishedMapGen = true;
                }
                else
                {
                    moveRight();
                }
            }
            else if ((blockedIndex[currentIndex - MapHoehe]) == 1 && (blockedIndex[currentIndex + MapHoehe]) == 1 && (blockedIndex[currentIndex - 1]) == 1 && (blockedIndex[currentIndex + 1]) == 1) //Bewegung in keine Richtung möglich
            {
                finishedMapGen = true;
            }
        }


        foreach(GameObject obj in EnemyNodes)
        {

            Destroy(obj);
            GameObject newPathNode = Instantiate(NodePath);
            newPathNode.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            PathNodes.Add(newPathNode);
            
        }
        lengthCounterPath = PathNodes.Count;
        startTile = PathNodes[0];
        endTile = PathNodes[lengthCounterPath - 1];
        startTile.GetComponent<Renderer>().material = StartMaterial;
        endTile.GetComponent<Renderer>().material = EndMaterial;
        

    }
    
    private void moveDown()
    {
        nextIndex = currentIndex - MapHoehe;
        int currentPos = blockedIndex[currentIndex];
        int topPos = 0;
        int leftPos = 0;
        int rightPos = 0;
        blockedIndex[currentIndex] = 1;
        if(currentIndex + MapHoehe < MapHoehe * MapBREITE) // Überprüfung darauf, ob aktuelle Position am oberen Rand liegt
        {
            topPos = blockedIndex[currentIndex + MapHoehe];
            blockedIndex[currentIndex + MapHoehe] = 1;
        }
        if(currentIndex % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
        {
            leftPos = blockedIndex[currentIndex - 1];
            blockedIndex[currentIndex - 1] = 1;
        }
        if((currentIndex + 1) % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
        {
            rightPos = blockedIndex[currentIndex + 1];
            blockedIndex[currentIndex + 1] = 1;
        }
        bool schleifeAktiv = true;
        int schleifeCurrentIndex = nextIndex;
        int failCounter = 0;
        int rightLeftCounter = 0;
        while(schleifeAktiv == true)
        {
            for (int i = schleifeCurrentIndex - MapBREITE; i > -1 ; i=i-MapBREITE)
            {
                if(blockedIndex[i] == 0)
                {
                    schleifeCurrentIndex = i;
                    failCounter = 0;
                    rightLeftCounter = 0;
                }
                if(blockedIndex[i] == 1)
                {
                    failCounter++;
                    i = -1;
                }
                if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE )
                {
                    schleifeAktiv = false;
                    i = -1;
                    moved = true;
                }
            }
            if (schleifeCurrentIndex % MapBREITE != 0)
            {
                for (int i = schleifeCurrentIndex - 1; i > -1; i = i - 1)
                {
                    if (blockedIndex[i] == 0)
                    {
                        schleifeCurrentIndex = i;
                        failCounter = 0;
                        rightLeftCounter++;
                    }
                    if (blockedIndex[i] == 1)
                    {
                        failCounter++;
                        i = -1;
                    }
                    if(schleifeCurrentIndex % MapBREITE == 0)
                    {
                        i = -1;
                    }
                    if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                    {
                        schleifeAktiv = false;
                        i = -1;
                        currentTile = MapNodes[nextIndex];
                        moved = true;
                    }
                }
            }
            for (int i = schleifeCurrentIndex - MapBREITE; i > -1; i = i - MapBREITE)
            {
                if (blockedIndex[i] == 0)
                {
                    schleifeCurrentIndex = i;
                    failCounter = 0;
                    rightLeftCounter = 0;
                }
                if (blockedIndex[i] == 1)
                {
                    failCounter++;
                    i = -1;
                }
                if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                {
                    schleifeAktiv = false;
                    i = -1;
                    moved = true;
                }
            }
            if((schleifeCurrentIndex + 1)%MapBREITE != 0)
            {
                for (int i = schleifeCurrentIndex + 1; i < MapBREITE * MapHoehe - 1; i++)
                {
                    if(blockedIndex[i] == 0)
                    {
                        schleifeCurrentIndex = i;
                        failCounter = 0;
                        rightLeftCounter++;
                    }
                    if(blockedIndex[i] == 1)
                    {
                        failCounter++;
                        i = MapBREITE * MapHoehe;
                    }
                    if((schleifeCurrentIndex + 1)%MapBREITE == 0)
                    {
                        i = MapBREITE * MapHoehe;
                    }
                    if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                    {
                        schleifeAktiv = false;
                        i = MapBREITE * MapHoehe;
                        moved = true;
                    }
                }
            }
            if (failCounter > 10 && moved == false)
            {
                schleifeAktiv = false;
                blockedIndex[currentIndex] = currentPos;
                if (currentIndex + MapHoehe < MapHoehe * MapBREITE) // Überprüfung darauf, ob aktuelle Position am oberen Rand liegt
                {
                    blockedIndex[currentIndex + MapHoehe] = topPos;
                }
                if (currentIndex % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    blockedIndex[currentIndex - 1] = leftPos;
                }
                if ((currentIndex + 1) % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    blockedIndex[currentIndex + 1] = rightPos;
                }
                Debug.Log("ACTION PREVENTED: MOVING DOWN");
            }
            if (rightLeftCounter > 2 * MapBREITE)
            {
                schleifeAktiv = false;
                blockedIndex[currentIndex] = currentPos;
                if (currentIndex + MapHoehe < MapHoehe * MapBREITE) // Überprüfung darauf, ob aktuelle Position am oberen Rand liegt
                {
                    blockedIndex[currentIndex + MapHoehe] = topPos;
                }
                if (currentIndex % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    blockedIndex[currentIndex - 1] = leftPos;
                }
                if ((currentIndex + 1) % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    blockedIndex[currentIndex + 1] = rightPos;
                }
                Debug.Log("ACTION PREVENTED: MOVING DOWN, RIGHT-LEFT EXCEPTION");
            }
        }
        if (moved == true)
        {
            currentTile = MapNodes[nextIndex];
            Debug.Log("Moved Down");
        }
    }
    
    private void moveUp()
    {
        nextIndex = currentIndex + MapHoehe;
        int currentPos = blockedIndex[currentIndex];
        int botPos = blockedIndex[currentIndex - MapHoehe];
        int leftPos = 0;
        int rightPos = 0;
        blockedIndex[currentIndex] = 1;
        blockedIndex[currentIndex - MapHoehe] = 1;
        if(currentIndex % MapBREITE != 0)
        {
            leftPos = blockedIndex[currentIndex - 1];
            blockedIndex[currentIndex - 1] = 1;
        }
        if((currentIndex + 1) % MapBREITE != 0)
        {
            rightPos = blockedIndex[currentIndex + 1];
            blockedIndex[currentIndex + 1] = 1;
        }
        bool schleifeAktiv = true;
        int schleifeCurrentIndex = nextIndex;
        int failCounter = 0;
        int rightLeftCounter = 0;
        while (schleifeAktiv == true)
        {
            for (int i = schleifeCurrentIndex - MapBREITE; i > -1; i = i - MapBREITE)
            {
                if (blockedIndex[i] == 0)
                {
                    schleifeCurrentIndex = i;
                    failCounter = 0;
                    rightLeftCounter = 0;
                }
                if (blockedIndex[i] == 1)
                {
                    failCounter++;
                    i = -1;
                }
                if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                {
                    schleifeAktiv = false;
                    i = -1;
                    moved = true;
                }
            }
            if (schleifeCurrentIndex % MapBREITE != 0)
            {
                for (int i = schleifeCurrentIndex - 1; i > -1; i = i - 1)
                {
                    if (blockedIndex[i] == 0)
                    {
                        schleifeCurrentIndex = i;
                        failCounter = 0;
                        rightLeftCounter++;
                    }
                    if (blockedIndex[i] == 1)
                    {
                        failCounter++;
                        i = -1;
                    }
                    if (schleifeCurrentIndex % MapBREITE == 0)
                    {
                        i = -1;
                    }
                    if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                    {
                        schleifeAktiv = false;
                        i = -1;
                        currentTile = MapNodes[nextIndex];
                        moved = true;
                    }
                }
            }
            for (int i = schleifeCurrentIndex - MapBREITE; i > -1; i = i - MapBREITE)
            {
                if (blockedIndex[i] == 0)
                {
                    schleifeCurrentIndex = i;
                    failCounter = 0;
                    rightLeftCounter = 0;
                }
                if (blockedIndex[i] == 1)
                {
                    failCounter++;
                    i = -1;
                }
                if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                {
                    schleifeAktiv = false;
                    i = -1;
                    moved = true;
                }
            }
            if ((schleifeCurrentIndex + 1) % MapBREITE != 0)
            {
                for (int i = schleifeCurrentIndex + 1; i < MapBREITE * MapHoehe - 1; i++)
                {
                    if (blockedIndex[i] == 0)
                    {
                        schleifeCurrentIndex = i;
                        failCounter = 0;
                        rightLeftCounter++;
                    }
                    if (blockedIndex[i] == 1)
                    {
                        failCounter++;
                        i = MapBREITE * MapHoehe;
                    }
                    if ((schleifeCurrentIndex + 1) % MapBREITE == 0)
                    {
                        i = MapBREITE * MapHoehe;
                    }
                    if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                    {
                        schleifeAktiv = false;
                        i = MapBREITE * MapHoehe;
                        moved = true;
                    }
                }
            }
            if (failCounter > 10 && moved == false)
            {
                schleifeAktiv = false;
                blockedIndex[currentIndex] = currentPos;
                blockedIndex[currentIndex - MapHoehe] = botPos;
                if (currentIndex % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    blockedIndex[currentIndex - 1] = leftPos;
                }
                if ((currentIndex + 1) % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    blockedIndex[currentIndex + 1] = rightPos;
                }
                Debug.Log("ACTION PREVENTED: MOVING UP");
            }
            if (rightLeftCounter > 2 * MapBREITE)
            {
                schleifeAktiv = false;
                blockedIndex[currentIndex] = currentPos;
                blockedIndex[currentIndex - MapHoehe] = botPos;
                if (currentIndex % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    blockedIndex[currentIndex - 1] = leftPos;
                }
                if ((currentIndex + 1) % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    blockedIndex[currentIndex + 1] = rightPos;
                }
                Debug.Log("ACTION PREVENTED: MOVING UP, RIGHT-LEFT EXCEPTION");
            }
        }
        if (moved == true)
        {
            currentTile = MapNodes[nextIndex];
            Debug.Log("Moved Up");
        }
    }
    
    private void moveLeft()
    {
        nextIndex = currentIndex-1;
        int currentPos = blockedIndex[currentIndex];
        int topPos = blockedIndex[currentIndex + MapHoehe];
        int botPos = blockedIndex[currentIndex - MapHoehe];
        int rightPos = 0;
        blockedIndex[currentIndex] = 1;
        blockedIndex[currentIndex + MapHoehe] = 1;
        blockedIndex[currentIndex - MapHoehe] = 1;
        if((currentIndex + 1) % MapBREITE != 0)
        {
            rightPos = blockedIndex[currentIndex + 1];
            blockedIndex[currentIndex + 1] = 1;
        }
        bool schleifeAktiv = true;
        int schleifeCurrentIndex = nextIndex;
        int failCounter = 0;
        int rightLeftCounter = 0;
        while (schleifeAktiv == true)
        {
            for (int i = schleifeCurrentIndex - MapBREITE; i > -1; i = i - MapBREITE)
            {
                if (blockedIndex[i] == 0)
                {
                    schleifeCurrentIndex = i;
                    failCounter = 0;
                    rightLeftCounter = 0;
                }
                if (blockedIndex[i] == 1)
                {
                    failCounter++;
                    i = -1;
                }
                if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                {
                    schleifeAktiv = false;
                    i = -1;
                    moved = true;
                }
            }
            if (schleifeCurrentIndex % MapBREITE != 0)
            {
                for (int i = schleifeCurrentIndex - 1; i > -1; i = i - 1)
                {
                    if (blockedIndex[i] == 0)
                    {
                        schleifeCurrentIndex = i;
                        failCounter = 0;
                        rightLeftCounter++;
                    }
                    if (blockedIndex[i] == 1)
                    {
                        failCounter++;
                        i = -1;
                    }
                    if (schleifeCurrentIndex % MapBREITE == 0)
                    {
                        i = -1;
                    }
                    if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                    {
                        schleifeAktiv = false;
                        i = -1;
                        currentTile = MapNodes[nextIndex];
                        moved = true;
                    }
                }
            }
            for (int i = schleifeCurrentIndex - MapBREITE; i > -1; i = i - MapBREITE)
            {
                if (blockedIndex[i] == 0)
                {
                    schleifeCurrentIndex = i;
                    failCounter = 0;
                    rightLeftCounter = 0;
                }
                if (blockedIndex[i] == 1)
                {
                    failCounter++;
                    i = -1;
                }
                if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                {
                    schleifeAktiv = false;
                    i = -1;
                    moved = true;
                }
            }
            if ((schleifeCurrentIndex + 1) % MapBREITE != 0)
            {
                for (int i = schleifeCurrentIndex + 1; i < MapBREITE * MapHoehe - 1; i++)
                {
                    if (blockedIndex[i] == 0)
                    {
                        schleifeCurrentIndex = i;
                        failCounter = 0;
                        rightLeftCounter++;
                    }
                    if (blockedIndex[i] == 1)
                    {
                        failCounter++;
                        i = MapBREITE * MapHoehe;
                    }
                    if ((schleifeCurrentIndex + 1) % MapBREITE == 0)
                    {
                        i = MapBREITE * MapHoehe;
                    }
                    if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                    {
                        schleifeAktiv = false;
                        i = MapBREITE * MapHoehe;
                        moved = true;
                    }
                }
            }
            if (failCounter > 10 && moved == false)
            {
                schleifeAktiv = false;
                blockedIndex[currentIndex] = currentPos;
                blockedIndex[currentIndex + MapHoehe] = topPos;
                blockedIndex[currentIndex - MapHoehe] = botPos;
                if ((currentIndex + 1) % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    blockedIndex[currentIndex + 1] = rightPos;
                }
                Debug.Log("ACTION PREVENTED: MOVING LEFT");
            }
            if (rightLeftCounter > 2 * MapBREITE)
            {
                schleifeAktiv = false;
                blockedIndex[currentIndex] = currentPos;
                blockedIndex[currentIndex + MapHoehe] = topPos;
                blockedIndex[currentIndex - MapHoehe] = botPos;
                if ((currentIndex + 1) % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am rechten Rand liegt
                {
                    blockedIndex[currentIndex + 1] = rightPos;
                }
                Debug.Log("ACTION PREVENTED: MOVING LEFT, RIGHT-LEFT EXCEPTION");
            }
        }
        if (moved == true)
        {
            currentTile = MapNodes[nextIndex];
            Debug.Log("Moved Left");
        }
    }
    private void moveRight()
    {
        nextIndex = currentIndex+1;
        int currentPos = blockedIndex[currentIndex];
        int topPos = blockedIndex[currentIndex + MapHoehe];
        int botPos = blockedIndex[currentIndex - MapHoehe];
        int leftPos = 0;
        blockedIndex[currentIndex] = 1;
        blockedIndex[currentIndex + MapHoehe] = 1;
        blockedIndex[currentIndex - MapHoehe] = 1;
        if(currentIndex % MapBREITE != 0)
        {
            leftPos = blockedIndex[currentIndex + 1];
            blockedIndex[currentIndex - 1] = 1;
        }
        bool schleifeAktiv = true;
        int schleifeCurrentIndex = nextIndex;
        int failCounter = 0;
        int rightLeftCounter = 0;
        while (schleifeAktiv == true)
        {
            for (int i = schleifeCurrentIndex - MapBREITE; i > -1; i = i - MapBREITE)
            {
                if (blockedIndex[i] == 0)
                {
                    schleifeCurrentIndex = i;
                    failCounter = 0;
                    rightLeftCounter = 0;
                }
                if (blockedIndex[i] == 1)
                {
                    failCounter++;
                    i = -1;
                }
                if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                {
                    schleifeAktiv = false;
                    i = -1;
                    moved = true;
                }
            }
            if (schleifeCurrentIndex % MapBREITE != 0)
            {
                for (int i = schleifeCurrentIndex - 1; i > -1; i = i - 1)
                {
                    if (blockedIndex[i] == 0)
                    {
                        schleifeCurrentIndex = i;
                        failCounter = 0;
                        rightLeftCounter++;
                    }
                    if (blockedIndex[i] == 1)
                    {
                        failCounter++;
                        i = -1;
                    }
                    if (schleifeCurrentIndex % MapBREITE == 0)
                    {
                        i = -1;
                    }
                    if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                    {
                        schleifeAktiv = false;
                        i = -1;
                        currentTile = MapNodes[nextIndex];
                        moved = true;
                    }
                }
            }
            for (int i = schleifeCurrentIndex - MapBREITE; i > -1; i = i - MapBREITE)
            {
                if (blockedIndex[i] == 0)
                {
                    schleifeCurrentIndex = i;
                    failCounter = 0;
                    rightLeftCounter = 0;
                }
                if (blockedIndex[i] == 1)
                {
                    failCounter++;
                    i = -1;
                }
                if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                {
                    schleifeAktiv = false;
                    i = -1;
                    moved = true;
                }
            }
            if ((schleifeCurrentIndex + 1) % MapBREITE != 0)
            {
                for (int i = schleifeCurrentIndex + 1; i < MapBREITE * MapHoehe - 1; i++)
                {
                    if (blockedIndex[i] == 0)
                    {
                        schleifeCurrentIndex = i;
                        failCounter = 0;
                        rightLeftCounter++;
                    }
                    if (blockedIndex[i] == 1)
                    {
                        failCounter++;
                        i = MapBREITE * MapHoehe;
                    }
                    if ((schleifeCurrentIndex + 1) % MapBREITE == 0)
                    {
                        i = MapBREITE * MapHoehe;
                    }
                    if (schleifeCurrentIndex == endTilePos || schleifeCurrentIndex == endTilePos + MapBREITE)
                    {
                        schleifeAktiv = false;
                        i = MapBREITE * MapHoehe;
                        moved = true;
                    }
                }
            }
            if (failCounter > 10 && moved == false)
            {
                schleifeAktiv = false;
                blockedIndex[currentIndex] = currentPos;
                blockedIndex[currentIndex + MapHoehe] = topPos;
                blockedIndex[currentIndex - MapHoehe] = botPos;
                if (currentIndex % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    blockedIndex[currentIndex - 1] = leftPos;
                }
                Debug.Log("ACTION PREVENTED: MOVING RIGHT");
            }
            if (rightLeftCounter > 2 * MapBREITE)
            {
                schleifeAktiv = false;
                blockedIndex[currentIndex] = currentPos;
                blockedIndex[currentIndex + MapHoehe] = topPos;
                blockedIndex[currentIndex - MapHoehe] = botPos;
                if (currentIndex % MapBREITE != 0) // Überprüfung darauf, ob aktuelle Position am linken Rand liegt
                {
                    blockedIndex[currentIndex - 1] = leftPos;
                }
                Debug.Log("ACTION PREVENTED: MOVING RIGHT, RIGHT-LEFT EXCEPTION");
            }
        }
        if (moved == true)
        {
            currentTile = MapNodes[nextIndex];
            Debug.Log("Moved Right");
        }
    }

}
