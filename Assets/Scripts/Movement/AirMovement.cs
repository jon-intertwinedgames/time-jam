using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovement : Movement
{
    public new void Move(Vector2 vel)
    {
        Move(vel.x, vel.y);
    }

    public new void Move(float xVel, float yVel)
    {
        xVel *= speed;
        yVel *= speed;
        base.Move(xVel, yVel);
    }
}
