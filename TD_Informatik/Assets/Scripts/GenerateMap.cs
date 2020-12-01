using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Node;

    private int MapBREITE = 9;
    private int MapHoehe = 9;

    private void MapGenerate()
    {
        for (float z = 0; z < 2 * MapBREITE; z = z + 2)
        {
            for (int x = 0; x < 2 * MapHoehe; x = x + 2)
            {
                GameObject newNode = Instantiate(Node);
                newNode.transform.position = new Vector3(x, 0, z);

            }
        }
    }
    private void Start()
    {
        MapGenerate();
    }
}
