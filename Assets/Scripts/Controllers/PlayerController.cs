using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

[RequireComponent (typeof(HandlePlayerState))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 1.75f;

    float minWaitTimeToShoot = 0.1f;

    [SerializeField]
    float minWaitTimeForPerfectArrow = 1f;

    [SerializeField]
    ParticleSystem particleSystem;
    EmissionModule particleSystemEmission;

    [SerializeField]
    private BowSFX bowSFX;

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

    public event Action TeleportEvent;

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

        particleSystemEmission = particleSystem.emission;
        particleSystemEmission.rateOverTime = 0;
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

        //if (h > 0)
        //    sr.flipX = false;
        //else if (h < 0)
        //    sr.flipX = true;

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

            if (rb.velocity.y == 0)
            {
                playerState_script.ActionState = HandlePlayerState.PlayerState.GroundShooting;
            }
            else
            {
                playerState_script.ActionState = HandlePlayerState.PlayerState.AirShooting;
            }
        }
        if (Input.GetButton("Fire1") && isAiming)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            aimer_script.SetTrajectory(mousePos);
            aimer_script.ShowTrajectory(true);

            particleSystemEmission.rateOverTime = 10f * GetPowerPercentage();//<-- LOOK HERE Magic number :D


            aimingTime += Time.deltaTime;
        }

        else if (Input.GetButtonUp("Fire1") && isAiming)
        {
            ReleaseBow();
                
            isAiming = false;
            bowSFX.StopPlayingSFX();
            aimingTime = 0;

            particleSystemEmission.rateOverTime = 0;
            ResetPlayerStateBackFromShooting();

            aimer_script.ShowTrajectory(false);
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

                TeleportEvent?.Invoke();
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
        float speedPercentage = GetPowerPercentage();

        var projectile = aimer_script.ShootProjectile(projectileDirection, "arrow", speedPercentage);
        var projectile_Damager = projectile.GetComponent<Damager>();

        if (speedPercentage > 5f)
        {
            projectile_Damager.setDamage(projectile_Damager.getDamage() * 2);
        }

        
    }

    void ResetPlayerStateBackFromShooting()
    {
        if (playerState_script.ActionState == HandlePlayerState.PlayerState.GroundShooting ||
            playerState_script.ActionState == HandlePlayerState.PlayerState.AirShooting)
        {
            playerState_script.ActionState = HandlePlayerState.PlayerState.Idle;
        }
    }

    private float GetPowerPercentage()
    {
        float speedPercentage = aimingTime / minWaitTimeForPerfectArrow;

        if (speedPercentage > 10f)//small hard cap
            speedPercentage = 10f;
        return speedPercentage;
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
