using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private HandlePlayerState playerState_script = null;

    private AudioSource flyingAudioSource = null;
    private AudioSource timeAudioSource = null;

    private void Awake()
    {
        playerState_script = GetComponent<HandlePlayerState>();
    }

    private void Start()
    {        
        flyingAudioSource = AudioManager.CreateAudioSource(transform);
        timeAudioSource = AudioManager.CreateAudioSource(transform);
    }

    private void Update()
    {
        if(playerState_script.ActionState == HandlePlayerState.PlayerState.Flying)
        {
            if(flyingAudioSource.isPlaying == false)
                FlyingInAirSFX();
        }
        else if(playerState_script.ActionState != HandlePlayerState.PlayerState.Flying)
        {
            flyingAudioSource.Stop();
        }

        if(Time.timeScale != 1 && timeAudioSource.isPlaying == false)
        {
            SlowingDownTimeSFX();
        }
        else if(Time.timeScale == 1)
        {
            timeAudioSource.Stop();
        }
    }

    private void RunningSFX()
    {
        AudioManager.PlayRandomOneShotSFX(SFX.Running1, SFX.Running2, SFX.Running3);
    }

    private void JumpingSFX()
    {
        AudioManager.PlayOneShotSFX(SFX.Jumping);
    }

    private void FlyingInAirSFX()
    {
        AudioManager.PlayRandomSFX(flyingAudioSource, true, SFX.Flying1, SFX.Flying2, SFX.Flying3, SFX.Flying4);
    }

    private void SlowingDownTimeSFX()
    {
        AudioManager.PlayRandomSFX(timeAudioSource, false, SFX.SlowingDownTime1, SFX.SlowingDownTime2, SFX.SlowingDownTime3, SFX.SlowingDownTime4);
    }

    public void TeleportingSFX()
    {        
        AudioManager.PlayRandomOneShotSFX(SFX.Teleporting1, SFX.Teleporting2, SFX.Teleporting3, SFX.Teleporting4, SFX.Teleporting5);
    }
}