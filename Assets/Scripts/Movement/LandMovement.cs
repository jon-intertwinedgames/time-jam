using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LandMovement : Movement
{
    [SerializeField] private float jumpSpeed = 0;

    private GroundDetector groundDetector_script;

    private float horizontalJumpSpeedCap = 0;
    private bool canJump = true;
    public bool CanJump { get => canJump; }

    protected override void Awake()
    {
        base.Awake();
        horizontalJumpSpeedCap = speed;
    }

    private void Start()
    {
        groundDetector_script = GetComponentInChildren<GroundDetector>();
    }

    public void Move(float xVelMultiplier)
    {
        xVelMultiplier *= speed;
        base.Move(xVelMultiplier, rb.velocity.y);

        //////if you want the same logic, add air friction
        //else
            //ForceMobility(xVelMultiplier);
    }

    private void ForceMobility(float xVelMultiplier)
    {
        xVelMultiplier *= speed;

        if(xVelMultiplier > 0 && rb.velocity.x < horizontalJumpSpeedCap)
            rb.AddForce(new Vector2(xVelMultiplier, 0), ForceMode2D.Force);
        else if (xVelMultiplier < 0 && rb.velocity.x > -horizontalJumpSpeedCap)
            rb.AddForce(new Vector2(xVelMultiplier, 0), ForceMode2D.Force);
    }

    public void Jump()
    {
        if (canJump == true)
        {
            if (groundDetector_script.IsOnGround)
            {
                AudioManager.PlayOneShotSFX(SFX.Jumping);
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                StartCoroutine(AllowAJump());
            }
        }
    }

    //I'm OCD and would like to guarantee that the character is no longer touching the ground before allowing for a jump again
    private IEnumerator AllowAJump()
    {
        canJump = false;

        for (int i = 0; i < 2; i++)
            yield return new WaitForFixedUpdate();

        canJump = true;
    }
}
