﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private HandlePlayerState playerState_script = null;

    private AudioSource audioSource = null;
    private AudioSource flyingAudioSource = null;
    private AudioSource timeAudioSource = null;

    private bool timeSlowAudioPlayed;

    [SerializeField]
    private AudioOptions running, jumping, flyingInAir, landing, slowingTime, teleporting;

    public AudioOptions Landing{ get => landing; }

    private void Awake()
    {
        playerState_script = GetComponent<HandlePlayerState>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {        
        flyingAudioSource = AudioManager.CreateAudioSource(transform);
        timeAudioSource = AudioManager.CreateAudioSource(transform);

        flyingAudioSource.gameObject.name = "flyingAudioSource";
        timeAudioSource.gameObject.name = "timeAudioSource";
    }

    private void Update()
    {
        if(playerState_script.ActionState == HandlePlayerState.PlayerState.Flying)
        {
            if (flyingAudioSource.isPlaying == false)
            {
                FlyingInAirSFX();
            }
        }
        else if(playerState_script.ActionState != HandlePlayerState.PlayerState.Flying)
        {
            flyingAudioSource.Stop();
        }

        if(Time.timeScale != 1 && timeSlowAudioPlayed == false)
        {
            SlowingDownTimeSFX();
            timeSlowAudioPlayed = true;
        }
        else if(Time.timeScale == 1 && timeSlowAudioPlayed)
        {
            timeAudioSource.Stop();
            timeSlowAudioPlayed = false;
        }
    }

    private void RunningSFX()
    {
        //AudioManager.PlayRandomOneShotSFX(SFX.Running1, SFX.Running2, SFX.Running3);
        AudioManager.PlayOneShotSFX(audioSource, running.Volume, running.Delay, SFX.Running1);
    }

    private void JumpingSFX()
    {
        AudioManager.PlayOneShotSFX(audioSource, jumping.Volume, jumping.Delay, SFX.Jumping);
    }

    private void FlyingInAirSFX()
    {
        AudioManager.PlaySFX(flyingAudioSource, flyingInAir.Volume, flyingInAir.Delay, true, SFX.Flying3);
        //AudioManager.PlayRandomSFX(flyingAudioSource, true, SFX.Flying1, SFX.Flying2, SFX.Flying3, SFX.Flying4);
    }

    private void SlowingDownTimeSFX()
    {
        AudioManager.PlaySFX(timeAudioSource, slowingTime.Volume, slowingTime.Delay, false, SFX.SlowingDownTime);
        //AudioManager.PlayRandomSFX(timeAudioSource, false, SFX.SlowingDownTime1, SFX.SlowingDownTime2, SFX.SlowingDownTime3, SFX.SlowingDownTime4);
    }

    public void TeleportingSFX()
    {
        AudioManager.PlayOneShotSFX(audioSource, teleporting.Volume, teleporting.Delay, SFX.Teleporting4);
       //AudioManager.PlayRandomOneShotSFX(SFX.Teleporting1, SFX.Teleporting2, SFX.Teleporting3, SFX.Teleporting4, SFX.Teleporting5);
    }
}