using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class End : MonoBehaviour
{
    public static TMP_Text endscore;
    int track;
    // Start is called before the first frame update
    void Start()
    {
        endscore = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        track = Points.points;
        endscore.text = "Score: " + track.ToString();
    }
}
