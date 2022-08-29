using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip GunShot, Powup1, Powup2, Powup3, TankDie;
    static AudioSource soundsorce;
    void Start()
    {
        GunShot = Resources.Load<AudioClip>("GunShot");
        Powup1 = Resources.Load<AudioClip>("Powup1");
        Powup2 = Resources.Load<AudioClip>("Powup2");
        Powup3 = Resources.Load<AudioClip>("Powup3");
        TankDie = Resources.Load<AudioClip>("TankDie");

        soundsorce = GetComponent<AudioSource>();     
    }
    public static void playsound(string clip)
    {
        switch (clip)
        {
            case "GunShot":
                soundsorce.PlayOneShot(GunShot);
                break;
            case "Powup1":
                soundsorce.PlayOneShot(Powup1);
                break;
            case "Powup2":
                soundsorce.PlayOneShot(Powup2);
                break;
            case "Powup3":
                soundsorce.PlayOneShot(Powup3);
                break;
            case "TankDie":
                soundsorce.PlayOneShot(TankDie);
                break;

        }
    }
}
