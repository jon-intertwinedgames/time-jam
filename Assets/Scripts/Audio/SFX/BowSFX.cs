using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSFX : MonoBehaviour
{
    AudioSource audioSource = null;
    //LineRenderer lr = null;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        /*if(lr.enabled && audioSource.isPlaying == false)
        {
            DrawingBowSFX();
        }
        else if(lr.enabled == false)
        {
            AudioManager.StopPlaying(audioSource);
        }*/
    }

    public void PlayDrawingBowSFX()
    {
        AudioManager.PlayRandomOneShotSFX(audioSource, SFX.BowDrawing1, SFX.BowDrawing2);
    }

    public void StopPlayingSFX()
    {
        AudioManager.StopPlaying(audioSource);
    }
}