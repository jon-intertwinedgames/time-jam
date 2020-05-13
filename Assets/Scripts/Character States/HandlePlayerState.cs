using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerState : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private string currentTrigger = "";


    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Landing,
        GroundShooting,
        AirShooting,
        Soaring,
        Death
    }

    private PlayerState actionState;
    public PlayerState ActionState
    {
        get => actionState;

        set
        {
            if (actionState != value)
            {
                if (actionState == PlayerState.Falling && (value == PlayerState.Idle || value == PlayerState.Running))
                    AudioManager.PlayOneShotSFX(SFX.NormalLanding);

                actionState = value;

                if (currentTrigger != "")
                {
                    anim.ResetTrigger(currentTrigger);
                }

                switch (actionState)
                {
                    case PlayerState.Idle:
                        currentTrigger = "Idle";
                        break;
                    case PlayerState.Running:
                        currentTrigger = "Running";
                        break;
                    case PlayerState.Jumping:
                        currentTrigger = "Jumping";
                        break;
                    case PlayerState.Falling:
                        break;
                    case PlayerState.Landing:
                        //AudioManager.PlayOneShot(SFX.NormalLanding);
                        break;
                    case PlayerState.GroundShooting:
                        currentTrigger = "Ground Shooting";
                        AudioManager.PlayOneShotSFX(SFX.Shooting);
                        break;
                    case PlayerState.AirShooting:
                        currentTrigger = "Air Shooting";
                        AudioManager.PlayOneShotSFX(SFX.Shooting);
                        break;
                    case PlayerState.Soaring:
                        break;
                    case PlayerState.Death:
                        break;
                }

                anim.SetTrigger(currentTrigger);
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (rb.velocity == Vector2.zero)
            ActionState = PlayerState.Idle;
        else if (rb.velocity.x != 0 && rb.velocity.y == 0)
            ActionState = PlayerState.Running;
        else if (rb.velocity.y > 0 && actionState != PlayerState.Soaring) //Separate this into Falling later.
            ActionState = PlayerState.Jumping;
        else if (rb.velocity.y < 0 && actionState != PlayerState.Soaring)
            ActionState = PlayerState.Falling;
    }
}
