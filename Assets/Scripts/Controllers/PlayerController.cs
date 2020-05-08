using System;
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

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        movement_script = GetComponent<LandMovement>();
        aimer_script = GetComponentInChildren<Aimer>();
        time_script = GetComponent<TimeManipulation>();
        playerState_script = GetComponent<HandlePlayerState>();
        health_script = GetComponent<Health>();

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

        if (Input.GetKeyDown(KeyCode.T))
        {
            health_script.TakeDamage(10);
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
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimer_script.ShowTrajectory(mousePos);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            ReleaseBow();
        }
    }

    void TeleportationInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Arrow.AllArrows.Count > 0)
            {
                GameObject closestArrow = Arrow.FindClosestArrowToCursor();
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

        playerState_script.ActionState = HandlePlayerState.PlayerState.Soaring;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
