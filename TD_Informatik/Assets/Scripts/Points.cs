using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public static Text PointsShow;
    public static int points;
    // Start is called before the first frame update
    void Start()
    {
        PointsShow = GetComponent<Text>();
        PointsShow.text = "Points: " + points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        PointsShow.text = "Points: " + points.ToString();
    }
}
