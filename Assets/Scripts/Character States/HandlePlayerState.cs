using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerState : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private GroundDetector groundDetector_script;

    private PlayerSFX playersfx_script;

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
        Flying,
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
                {
                    playersfx_script.LandingSFX();
                }

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
                        break;
                    case PlayerState.AirShooting:
                        currentTrigger = "Air Shooting";
                        break;
                    case PlayerState.Flying:
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

    private void Start()
    {
        groundDetector_script = GetComponentInChildren<GroundDetector>();
        playersfx_script = GetComponent<PlayerSFX>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (ActionState == PlayerState.GroundShooting)
        {
            if (rb.velocity.y != 0)
                ActionState = PlayerState.AirShooting;

            else
                ResetRotation();

            return;
        }

        if(ActionState == PlayerState.AirShooting)
        {
            if (rb.velocity.y == 0)
            {
                ActionState = PlayerState.GroundShooting;
            }
            else
            {
                LookatMouse();
            }
            return;
        }

        ResetRotation();
        if (ActionState == PlayerState.Death)
            return;

        if (rb.velocity == Vector2.zero && groundDetector_script.IsOnGround)
            ActionState = PlayerState.Idle;
        else if (rb.velocity.x != 0 && rb.velocity.y == 0 && groundDetector_script.IsOnGround)
            ActionState = PlayerState.Running;
        else if (rb.velocity.y > 0 && actionState != PlayerState.Flying) //Separate this into Falling later.
            ActionState = PlayerState.Jumping;
        else if (rb.velocity.y < 0 && actionState != PlayerState.Flying)
            ActionState = PlayerState.Falling;
    }

    private void LookatMouse()
    {
        //GetComponent<FlipObjectBasedOnRigidbody>().enabled = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int offset = 0;

        if (this.transform.localScale.x < 0)
            offset = 180;

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg + offset);
    }

    private void ResetRotation()
    {
        //GetComponent<FlipObjectBasedOnRigidbody>().enabled = true;
        transform.rotation = new Quaternion();
    }
}
