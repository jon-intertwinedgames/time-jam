using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSFX : MonoBehaviour
{

    System.Random ran = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<AudioSource>() != null)
        {
            SFX_Init_RandomFlyingArrow();
        }
    }

    public void SFX_Init_FlyingArrow1()
    {
        GetComponent<AudioSource>().clip = AudioManager.getSFXClip(SFX.FlyingArrow1);
        GetComponent<AudioSource>().Play();
    }

    public void SFX_Init_FlyingArrow2()
    {
        GetComponent<AudioSource>().clip = AudioManager.getSFXClip(SFX.FlyingArrow2);
        GetComponent<AudioSource>().Play();
    }

    public void SFX_Init_FlyingArrow3()
    {
        GetComponent<AudioSource>().clip = AudioManager.getSFXClip(SFX.FlyingArrow3);
        GetComponent<AudioSource>().Play();
    }

    public void SFX_Init_FlyingArrow4()
    {
        GetComponent<AudioSource>().clip = AudioManager.getSFXClip(SFX.FlyingArrow4);
        GetComponent<AudioSource>().Play();
    }

    public void SFX_Init_RandomFlyingArrow()
    {
        int i = ran.Next(0, 4);

        switch (i)
        {
            case 0:
                SFX_Init_FlyingArrow1();
                break;
            case 1:
                SFX_Init_FlyingArrow2();
                break;
            case 2:
                SFX_Init_FlyingArrow3();
                break;
            default:
                SFX_Init_FlyingArrow4();
                break;
        }
    }


}
