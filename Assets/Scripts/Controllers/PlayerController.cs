using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

[RequireComponent (typeof(HandlePlayerState))]
public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    //private float fireRate = 1.75f;

    float minWaitTimeToShoot = 0.1f;

    [SerializeField]
    float minWaitTimeForPerfectArrow = 1f;

    [SerializeField]
    float maxSpeedOfArrow = 8f;

    [SerializeField]
    new ParticleSystem particleSystem = null;
    EmissionModule particleSystemEmission;

    [SerializeField] private BowSFX bowSFX = null;
    [SerializeField] private GameObject deathAnim = null;

    private LandMovement movement_script;
    private Aimer aimer_script;
    private TimeManipulation time_script;
    private HandlePlayerState playerState_script;
    private Health health_script;
    private GroundDetector groundDetector_script;
    private FlipObjectBasedOnRigidbody flipObject_script;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isAiming;
    private float aimingTime = 0;

    public event Action IdleEvent, RunningEvent, JumpingEvent, FallingEvent,
                                    GroundAimEvent, GroundShootEvent,
                                    AirAimEvent, AirShootEvent,
                                    TeleportEvent, MovingGroundAimEvent, MovingGroundShootEvent;

    void Start()
    {
        movement_script = GetComponent<LandMovement>();
        aimer_script = GetComponentInChildren<Aimer>();
        time_script = GetComponent<TimeManipulation>();
        playerState_script = GetComponent<HandlePlayerState>();
        health_script = GetComponent<Health>();
        groundDetector_script = GetComponentInChildren<GroundDetector>();
        flipObject_script = GetComponent<FlipObjectBasedOnRigidbody>();

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        health_script.DeathEvent += Death;

        particleSystemEmission = particleSystem.emission;
        particleSystemEmission.rateOverTime = 0;
    }

    void Update()
    {
        if (GameMaster.State == GameState.Playing)
        {
            MovementInput();
            ArrowInput();
            TeleportationInput();
            TimeInput();

            flipObject_script.enabled = (groundDetector_script.IsOnGround) ? true : false;

            if (Debug.isDebugBuild)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    health_script.ChangeHealth(-10);
                }
            }
        }
    }

    void MovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Jump");

        movement_script.Move(h);


        bool willJump = false;

        if (v != 0)
        {
            willJump = movement_script.Jump();
        }


        if (playerState_script.ActionState == HandlePlayerState.PlayerState.AirAiming ||
            playerState_script.ActionState == HandlePlayerState.PlayerState.GroundAiming ||
            playerState_script.ActionState == HandlePlayerState.PlayerState.AirShooting||
            playerState_script.ActionState == HandlePlayerState.PlayerState.GroundShooting ||
            playerState_script.ActionState == HandlePlayerState.PlayerState.MovingGroundAim ||
            playerState_script.ActionState == HandlePlayerState.PlayerState.MovingGroundShoot)
        {
            return;
        }

        if (h == 0 && groundDetector_script.IsOnGround)
        {
            IdleEvent?.Invoke();
        }

        else if (h != 0 && groundDetector_script.IsOnGround && playerState_script.ActionState != HandlePlayerState.PlayerState.Jumping)
        {
            RunningEvent?.Invoke();
        }

        if (rb.velocity.y < 0 && groundDetector_script.IsOnGround == false)
        {
            FallingEvent?.Invoke();
        }

        if (willJump)
        {
            JumpingEvent?.Invoke();
        }
    }
    
    void ArrowInput()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            isAiming = true;
            bowSFX.PlayDrawingBowSFX();
            StopAllCoroutines();
        }
        if (Input.GetButton("Fire1") && isAiming)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            aimer_script.SetTrajectory(mousePos);
            aimer_script.ShowTrajectory(true);

            particleSystemEmission.rateOverTime = 5f * GetPowerPercentage();//<-- LOOK HERE Magic number :D

            if (groundDetector_script.IsOnGround)
            {
                if(GetComponent<Rigidbody2D>().velocity.x != 0)
                {
                    MovingGroundAimEvent?.Invoke();
                }
                else
                {

                    GroundAimEvent?.Invoke();
                }
            }
            else
            {
                AirAimEvent?.Invoke();
            }

            aimingTime += Time.deltaTime;
        }

        else if (Input.GetButtonUp("Fire1") && isAiming)
        {
            ReleaseBow();
                
            isAiming = false;
            bowSFX.StopPlayingSFX();
            aimingTime = 0;

            particleSystemEmission.rateOverTime = 0;

            aimer_script.ShowTrajectory(false);

            if(groundDetector_script.IsOnGround)
            {
                if (GetComponent<Rigidbody2D>().velocity.x != 0)
                {
                    MovingGroundShootEvent?.Invoke();
                }
                else
                {
                    GroundShootEvent?.Invoke();
                }
            }
            else
            {
                AirShootEvent?.Invoke();
            }

            StartCoroutine(resetToNormal());
        }
    }

    IEnumerator resetToNormal()
    {
        yield return new WaitForSeconds(0.1f);
        IdleEvent?.Invoke();
    }

    void TeleportationInput()
    {
        if (Input.GetButtonDown("Teleport"))
        {
            if (Arrow.AllArrows.Count > 0)
            {
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

    private float GetPowerPercentage()
    {
        float speedPercentage = aimingTime / minWaitTimeForPerfectArrow;

        if (speedPercentage > maxSpeedOfArrow) //small hard cap
            speedPercentage = maxSpeedOfArrow;
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

    private void Death()
    {
        Instantiate(deathAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);//gamemaster is always searching for this obj
    }
}
