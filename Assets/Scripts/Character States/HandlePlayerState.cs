using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerState : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private GroundDetector groundDetector_script;

    private PlayerController playerController_script;
    private PlayerSFX playersfx_script;

    private string currentTrigger = "";

    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Landing,
        GroundAiming,
        GroundShooting,
        AirAiming,
        AirShooting,
        Flying,
        Death,
        MovingGroundAim,
        MovingGroundShoot
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
                print(actionState.ToString());
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
                        currentTrigger = "Jumping"; //Change this to falling when we get the animation for it
                        break;
                    case PlayerState.Landing:
                        break;
                    case PlayerState.GroundAiming:
                        currentTrigger = "Ground Aiming";
                        break;
                    case PlayerState.GroundShooting:
                        currentTrigger = "Ground Shooting";
                        break;
                    case PlayerState.AirAiming:
                        currentTrigger = "Air Shooting";
                        break;
                    case PlayerState.AirShooting:
                        currentTrigger = "Air Shooting";
                        break;
                    case PlayerState.Flying:
                        currentTrigger = "Jumping"; //Change this into flying when we get the animation for it
                        break;
                    case PlayerState.Death:
                        break;
                    case PlayerState.MovingGroundAim:
                        currentTrigger = "Moving Ground Aiming"; //Change this into flying when we get the animation for it
                        break;
                    case PlayerState.MovingGroundShoot:
                        currentTrigger = "Moving Ground Shooting";
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
        playerController_script = GetComponent<PlayerController>();

        playerController_script.IdleEvent += delegate { ActionState = PlayerState.Idle; };
        playerController_script.RunningEvent += delegate { ActionState = PlayerState.Running; };
        playerController_script.JumpingEvent += delegate { ActionState = PlayerState.Jumping; };
        playerController_script.FallingEvent += delegate { ActionState = PlayerState.Falling; };
        playerController_script.GroundAimEvent += delegate { ActionState = PlayerState.GroundAiming; };
        playerController_script.GroundShootEvent += delegate { ActionState = PlayerState.GroundShooting; };
        playerController_script.AirAimEvent += delegate { ActionState = PlayerState.AirAiming; };
        playerController_script.AirShootEvent += delegate { ActionState = PlayerState.AirShooting; };
        playerController_script.MovingGroundAimEvent += delegate { ActionState = PlayerState.MovingGroundAim; };
        playerController_script.MovingGroundShootEvent += delegate { ActionState = PlayerState.MovingGroundShoot; };
    }

    private void Update()
    {
        UpdateState();
        //print(actionState);
    }

    private void UpdateState()
    {
        if(groundDetector_script.IsOnGround)
        {
            ResetRotation();
        }

        switch (actionState)
        {
            case PlayerState.AirAiming:
            case PlayerState.AirShooting:
                LookatMouse();
                break;
        }
    }

    private void LookatMouse()
    {
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
        transform.rotation = new Quaternion();
    }
}
