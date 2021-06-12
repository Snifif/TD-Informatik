using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Update()
    {
        if(GenerateMap.cameraAligned == false)
        {
            move(GenerateMap.cameraPosition, GenerateMap.cameraRotation);
            GenerateMap.cameraAligned = true;
        }
    }

    public void move(Vector3 position, Vector3 rotation)
    {
        this.transform.position = position;
        this.transform.rotation = Quaternion.Euler(rotation);
    } 
}
