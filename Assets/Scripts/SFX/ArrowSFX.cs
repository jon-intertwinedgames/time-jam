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
            SFX_Loop_RandomFlyingArrow();
        }
    }
    /// <summary>
    /// On Looping SFX 
    /// </summary>
    private void SFX_Loop_FlyingArrow1()
    {
        GetComponent<AudioSource>().clip = AudioManager.getSFXClip(SFX.FlyingArrow1);
        GetComponent<AudioSource>().Play();
    }

    private void SFX_Loop_FlyingArrow2()
    {
        GetComponent<AudioSource>().clip = AudioManager.getSFXClip(SFX.FlyingArrow2);
        GetComponent<AudioSource>().Play();
    }

    private void SFX_Loop_FlyingArrow3()
    {
        GetComponent<AudioSource>().clip = AudioManager.getSFXClip(SFX.FlyingArrow3);
        GetComponent<AudioSource>().Play();
    }

    private void SFX_Loop_FlyingArrow4()
    {
        GetComponent<AudioSource>().clip = AudioManager.getSFXClip(SFX.FlyingArrow4);
        GetComponent<AudioSource>().Play();
    }

    private void SFX_Loop_RandomFlyingArrow()
    {
        int i = ran.Next(0, 4);

        switch (i)
        {
            case 0:
                SFX_Loop_FlyingArrow1();
                break;
            case 1:
                SFX_Loop_FlyingArrow2();
                break;
            case 2:
                SFX_Loop_FlyingArrow3();
                break;
            default:
                SFX_Loop_FlyingArrow4();
                break;
        }
    }
    /// <summary>
    /// Collision SFX
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopLooping();
        HandleImpactSFX(collision);
    }

    private void StopLooping()
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Stop();
        }
    }

    private void HandleImpactSFX(Collider2D collision)
    {
        var str = LayerMask.LayerToName(collision.gameObject.layer);
        switch (str)
        {
            case "Terrain":
                SFX_Impact_ArrowHittingWall();
                break;
            default:
                break;
        }
    }

    private void SFX_Impact_ArrowHittingWall1()
    {
        AudioManager.PlayOneShotSFX(SFX.ArrowHittingWall1, this.transform.position);
    }
    private void SFX_Impact_ArrowHittingWall2()
    {
        AudioManager.PlayOneShotSFX(SFX.ArrowHittingWall2, this.transform.position);
    }
    private void SFX_Impact_ArrowHittingWall3()
    {
        AudioManager.PlayOneShotSFX(SFX.ArrowHittingWall3, this.transform.position);
    }
    private void SFX_Impact_ArrowHittingWall4()
    {
        AudioManager.PlayOneShotSFX(SFX.ArrowHittingWall4, this.transform.position);
    }
    private void SFX_Impact_ArrowHittingWall()
    {
        int i = ran.Next(0, 4);

        switch (i)
        {
            case 0:
                SFX_Impact_ArrowHittingWall1();
                break;
            case 1:
                SFX_Impact_ArrowHittingWall2();
                break;
            case 2:
                SFX_Impact_ArrowHittingWall3();
                break;
            default:
                SFX_Impact_ArrowHittingWall4();
                break;
        }
    }
}
