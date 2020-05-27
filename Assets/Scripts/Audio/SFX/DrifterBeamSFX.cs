using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterBeamSFX : MonoBehaviour
{
    AudioSource audioSource = null;

    [SerializeField]
    private AudioOptions beamShot, beamHit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        BeamShotSFX();
    }

    private void BeamShotSFX()
    {
        AudioManager.PlayRandomOneShotSFX(audioSource, beamShot.Volume, beamShot.Delay, SFX.DrifterBeamShooting1, SFX.DrifterBeamShooting2, SFX.DrifterBeamShooting3);
    }

    private void BeamHitSFX()
    {
        AudioManager.PlayRandomOneShotSFX(audioSource, beamShot.Volume, beamHit.Delay, SFX.DrifterBeamHit1, SFX.DrifterBeamHit2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            BeamHitSFX();
        }
    }
}
