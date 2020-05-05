using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LandMovement movement_script;
    private Aimer aimer_script;
    private TimeManipulation time_script;

    private Rigidbody2D rb;

    void Start()
    {
        movement_script = GetComponent<LandMovement>();
        aimer_script = GetComponentInChildren<Aimer>();
        time_script = GetComponent<TimeManipulation>();

        rb = GetComponent<Rigidbody2D>();
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

        movement_script.Move(h, v);
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

        //if (Input.GetMouseButtonDown(1))
        //    time_script.ToggleTimeScale();
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
    }
}
