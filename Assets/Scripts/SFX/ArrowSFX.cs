using System;
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
            RandomFunctionCall(SFX_Loop_FlyingArrow1, SFX_Loop_FlyingArrow2, SFX_Loop_FlyingArrow3, SFX_Loop_FlyingArrow4);
        }
    }
    ///////////////////////UTILS START
    /// <summary>
    /// Excutes one random function/action from the number of args
    /// </summary>
    /// <param name="sfxs"></param>
    private void RandomFunctionCall(params Action[] sfxs)
    {
        int i = ran.Next(0, sfxs.Length - 1);

        sfxs[i]();
    }
    ///////////////////////UTILS END

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
    
    /// <summary>
    /// Collision/Impact SFX
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
                 RandomFunctionCall(
                    SFX_Impact_ArrowHittingWall1,
                    SFX_Impact_ArrowHittingWall2,
                    SFX_Impact_ArrowHittingWall3,
                    SFX_Impact_ArrowHittingWall4);
                break;
            case "Enemies":
                RandomFunctionCall(
                    SFX_Impact_ArrowHittingFlesh2,
                    SFX_Impact_ArrowHittingFlesh3,
                    SFX_Impact_ArrowHittingFlesh4);
                break;
            default:
                Debug.LogError("IDK WHAT " + this.gameObject + "JUST HIT... Soo I can't play SFX :O");
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

    private void SFX_Impact_ArrowHittingFlesh1()
    {
        AudioManager.PlayOneShotSFX(SFX.ArrowHittingFlesh1, this.transform.position);
    }
    private void SFX_Impact_ArrowHittingFlesh2()
    {
        AudioManager.PlayOneShotSFX(SFX.ArrowHittingFlesh2, this.transform.position);
    }
    private void SFX_Impact_ArrowHittingFlesh3()
    {
        AudioManager.PlayOneShotSFX(SFX.ArrowHittingFlesh3, this.transform.position);
    }
    private void SFX_Impact_ArrowHittingFlesh4()
    {
        AudioManager.PlayOneShotSFX(SFX.ArrowHittingFlesh4, this.transform.position);
    }
}
