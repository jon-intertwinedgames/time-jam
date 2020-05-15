using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterBeamSFX : MonoBehaviour
{
    AudioSource audioSource = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        BeamShotSFX();
    }

    private void BeamShotSFX()
    {
        AudioManager.PlayRandomOneShotSFX(audioSource, SFX.DrifterBeamShooting1, SFX.DrifterBeamShooting2, SFX.DrifterBeamShooting3, SFX.DrifterBeamShooting4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            AudioManager.PlayRandomOneShotSFX(audioSource, SFX.DrifterBeamHit1, SFX.DrifterBeamHit2);
        }
    }
}
