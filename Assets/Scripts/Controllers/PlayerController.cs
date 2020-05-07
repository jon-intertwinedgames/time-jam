﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(HandlePlayerState))]
public class PlayerController : MonoBehaviour
{
    private LandMovement movement_script;
    private Aimer aimer_script;
    private TimeManipulation time_script;
    private HandlePlayerState playerState_script;
    private Health health_script;

    [SerializeField] private RectTransform healthBar_trans = null;

    private Rigidbody2D rb;

    [Header("Health")]
    private float healthBarReductionRate, healthOffSet;

    void Start()
    {
        movement_script = GetComponent<LandMovement>();
        aimer_script = GetComponentInChildren<Aimer>();
        time_script = GetComponent<TimeManipulation>();
        playerState_script = GetComponent<HandlePlayerState>();
        health_script = GetComponent<Health>();

        rb = GetComponent<Rigidbody2D>();

        UpdateHealth();
        healthBarReductionRate = healthBar_trans.rect.width / health_script.StartingHealth;
        healthOffSet = health_script.StartingHealth * healthBarReductionRate;
    }

    void Update()
    {
        MovementInput();
        ArrowInput();
        TeleportationInput();
        TimeInput();
    }

    void MovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Jump");

        movement_script.Move(h);

        if (v != 0)
            movement_script.Jump();
    }

    void ArrowInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimer_script.ShowTrajectory(mousePos);
        }            

        else if (Input.GetMouseButtonUp(0))
            ReleaseBow();
    }

    void TeleportationInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Transform[] allArrows = Arrow.GetAllActiveArrows();

            if (allArrows.Length > 0)
            {
                GameObject closestArrow = Arrow.FindClosestArrowToCursor(allArrows);
                TeleportToArrow(closestArrow);
            }
        }
    }

    void TimeInput()
    {
        if (Input.GetMouseButtonDown(1))
            time_script.StartSlowingDownTime();
        else if (Input.GetMouseButtonUp(1))
            time_script.ResetTimeScale();
    }

    private void ReleaseBow()
    {
        Vector2 mouseInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 projectileDirection = aimer_script.GetDirectionOfAim(mouseInWorldPoint);
        aimer_script.ShootProjectile(projectileDirection, "arrow");
    }

    private void TeleportToArrow(GameObject arrowToTeleportTo)
    {
        Vector2 arrowVelocity = arrowToTeleportTo.GetComponent<Rigidbody2D>().velocity;
        Destroy(arrowToTeleportTo);
        transform.position = arrowToTeleportTo.transform.position;
        rb.velocity = arrowVelocity;

        playerState_script.ActionState = HandlePlayerState.PlayerState.Soaring;
    }

    public void UpdateHealth()
    {
        Vector2 newHealthPos = healthBar_trans.localPosition;
        newHealthPos.x = health_script.CurrentHealth * healthBarReductionRate - healthOffSet;
        healthBar_trans.localPosition = newHealthPos;
    }
}
