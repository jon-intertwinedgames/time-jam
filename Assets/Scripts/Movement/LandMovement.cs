using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LandMovement : Movement
{
    [SerializeField]
    private float jumpSpeed = 0;

    private bool jumped;

    public override void Move(float xVel, float yVel)
    {
        if (yVel != 0 && jumped == false)
        {
            if(IsOnGround())
                Jump(yVel);
        }
            
        else if(yVel == 0)
            jumped = false;

        xVel *= speed;
        base.Move(xVel, rb.velocity.y);
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
