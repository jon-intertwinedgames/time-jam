using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LandMovement : Movement
{
    [SerializeField] private float jumpSpeed = 0;
    [SerializeField] private float horizontalJumpSpeedForce = 0;
    
    private float horizontalJumpSpeedCap = 0;
    private bool jumped;

    protected override void Awake()
    {
        base.Awake();
        horizontalJumpSpeedCap = speed;
    }

    public override void Move(float xVel, float yVel)
    {
        bool isOnGround = IsOnGround();

        if (yVel != 0 && jumped == false)
        {
            if(isOnGround)
                Jump(yVel);
        }
            
        else if(yVel == 0)
            jumped = false;

        if (isOnGround)
        {
            xVel *= speed;
            base.Move(xVel, rb.velocity.y);
        }
        else
            ForceMobility(xVel);
    }

    private void ForceMobility(float xVel)
    {
        xVel *= horizontalJumpSpeedForce;

        if(xVel > 0 && rb.velocity.x < horizontalJumpSpeedCap)
            rb.AddForce(new Vector2(xVel, 0), ForceMode2D.Force);
        else if (xVel < 0 && rb.velocity.x > -horizontalJumpSpeedCap)
            rb.AddForce(new Vector2(xVel, 0), ForceMode2D.Force);
    }

    private void Jump(float yVel)
    {
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumped = true;
    }

    private bool IsOnGround()
    {
        GroundDetector groundDetector_script = GetComponentInChildren<GroundDetector>();
        
        if(groundDetector_script.IsOnGround())
            return true;

        return false;
    }
}
