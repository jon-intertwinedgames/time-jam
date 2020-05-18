﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(HandlePlayerState))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float minWaitTimeToShoot = 0.1f;

    [SerializeField]
    float minWaitTimeForPerfectArrow = 1f;

    [SerializeField]
    BowSFX bowSFX;

    private LandMovement movement_script;
    private Aimer aimer_script;
    private TimeManipulation time_script;
    private HandlePlayerState playerState_script;
    private Health health_script;
    private PlayerSFX playerSFX_script;
    

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isAiming;
    private float aimingTime = 0;

    void Start()
    {
        movement_script = GetComponent<LandMovement>();
        aimer_script = GetComponentInChildren<Aimer>();
        time_script = GetComponent<TimeManipulation>();
        playerState_script = GetComponent<HandlePlayerState>();
        health_script = GetComponent<Health>();
        playerSFX_script = GetComponent<PlayerSFX>();

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        health_script.DeathEvent += Death;
    }

    void Update()
    {
        MovementInput();
        ArrowInput();
        TeleportationInput();
        TimeInput();

        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                health_script.ChangeHealth(-10);
            }
        }
        
    }

    void MovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Jump");

        if (h > 0)
            sr.flipX = false;
        else if (h < 0)
            sr.flipX = true;

        movement_script.Move(h);

        if (v != 0)
        {
            movement_script.Jump();
        }
    }
    
    void ArrowInput()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            isAiming = true;
            bowSFX.PlayDrawingBowSFX();
        }
        if (Input.GetButton("Fire1") && isAiming)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimer_script.ShowTrajectory(mousePos);

            aimingTime += Time.deltaTime;
        }

        else if (Input.GetButtonUp("Fire1") && isAiming)
        {
            ReleaseBow();
                
            isAiming = false;
            bowSFX.StopPlayingSFX();
            aimingTime = 0;
        }
    }

    void TeleportationInput()
    {
        if (Input.GetButtonDown("Teleport"))
        {
            if (Arrow.AllArrows.Count > 0)
            {
                playerSFX_script.TeleportingSFX();
                GameObject closestArrow = Arrow.FindClosestArrowToCursor();
                TeleportToArrow(closestArrow);
            }
        }
    }

    void TimeInput()
    {
        if (Input.GetButtonDown("Fire2"))
            time_script.StartSlowingDownTime();
        else if (Input.GetButtonUp("Fire2"))
            time_script.ResetTimeScale();
    }

    private void ReleaseBow()
    {
        if (aimingTime < minWaitTimeToShoot) return;

        Vector2 mouseInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 projectileDirection = aimer_script.GetDirectionOfAim(mouseInWorldPoint);

        float speedPercentage = aimingTime/minWaitTimeForPerfectArrow;

        if (speedPercentage > 10f)//small hard cap
            speedPercentage = 10f;

        var projectile = aimer_script.ShootProjectile(projectileDirection, "arrow", speedPercentage);
        var projectile_Damager = projectile.GetComponent<Damager>();

        if(speedPercentage > 5f)
        {
            projectile_Damager.setDamage(projectile_Damager.getDamage() * 2);
        }

        if (rb.velocity.y == 0)
        {
            playerState_script.ActionState = HandlePlayerState.PlayerState.GroundShooting;
        }
        else
        {
            playerState_script.ActionState = HandlePlayerState.PlayerState.AirShooting;
        }
    }

    private void TeleportToArrow(GameObject arrowToTeleportTo)
    {
        Rigidbody2D arrowRB = arrowToTeleportTo.GetComponent<Rigidbody2D>();

        if(arrowRB == null)
        {
            rb.velocity = Vector2.zero;            
        }
        else
        {
            Vector2 arrowVelocity = arrowToTeleportTo.GetComponent<Rigidbody2D>().velocity;
            rb.velocity = arrowVelocity;
        }

        transform.position = arrowToTeleportTo.transform.position;
        Destroy(arrowToTeleportTo);        

        playerState_script.ActionState = HandlePlayerState.PlayerState.Flying;
    }

    [SerializeField]
    private void Death()
    {
        Destroy(gameObject);
    }
}
