using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public static int hp = 200;
    public static Text HPTrack;
    // Start is called before the first frame update
    void Start()
    {
        HPTrack = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = EnemyBehavior2.playerdmg; i > 0; i--)
        {
            hp = hp - 20;
            if (hp == 0)
            {
                SceneManager.LoadScene("Overscreen");
            }
        }
        EnemyBehavior2.playerdmg = 0;
        HPTrack.text = "HP: " + hp.ToString();
    }
    public void Over()
    {
        SceneManager.LoadScene("Overscreen");
    }
}
