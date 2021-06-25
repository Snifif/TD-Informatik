using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource boom;
    public AudioSource bop;
    public AudioSource pOW;


    public void BOOM()
    {
        boom.Play();
    }

    public void BOP()
    {
        bop.Play();
    }

    public void POW()
    {
        pOW.Play();
    }

}
