using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private PlayerController playerController_script = null;
    private HandlePlayerState playerState_script = null;

    private AudioSource audioSource = null;
    private AudioSource flyingAudioSource = null;
    private AudioSource timeAudioSource = null;
    private AudioSource deathAudioSource = null;

    private bool timeSlowAudioPlayed;

    [SerializeField]
    private AudioOptions running, jumping, flyingInAir, landing, slowingTime, teleporting, death;

    public AudioOptions Landing{ get => landing; }

    private void Awake()
    {
        playerController_script = GetComponent<PlayerController>();
        playerState_script = GetComponent<HandlePlayerState>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {        
        flyingAudioSource = AudioManager.CreateAudioSource(transform);
        timeAudioSource = AudioManager.CreateAudioSource(transform);
        deathAudioSource = AudioManager.CreateAudioSource();

        flyingAudioSource.gameObject.name = "flyingAudioSource";
        timeAudioSource.gameObject.name = "timeAudioSource";

        playerController_script.TeleportEvent += TeleportingSFX;
        GetComponent<Health>().DeathEvent += DeathSFX;
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
        AudioManager.PlayOneShotSFX(audioSource, running.Volume, running.Delay, SFX.Running1);
    }

    private void JumpingSFX()
    {
        AudioManager.PlayOneShotSFX(audioSource, jumping.Volume, jumping.Delay, SFX.Jumping);
    }

    public void LandingSFX()
    {
        AudioManager.PlayOneShotSFX(audioSource, landing.Volume, landing.Delay, SFX.NormalLanding);
    }

    private void FlyingInAirSFX()
    {
        AudioManager.PlaySFX(flyingAudioSource, flyingInAir.Volume, flyingInAir.Delay, true, SFX.Flying3);
    }

    private void SlowingDownTimeSFX()
    {
        AudioManager.PlaySFX(timeAudioSource, slowingTime.Volume, slowingTime.Delay, false, SFX.SlowingDownTime);
    }

    private void TeleportingSFX()
    {
        AudioManager.PlayOneShotSFX(audioSource, teleporting.Volume, teleporting.Delay, SFX.Teleporting4);
    }

    private void DeathSFX()
    {
        deathAudioSource.transform.position = transform.position;
        AudioManager.PlayOneShotSFX(deathAudioSource, death.Volume, death.Delay, SFX.DrifterDeath);
    }
}