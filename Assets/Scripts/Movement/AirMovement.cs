using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMovement : Movement
{
    public override void Move(float xVel, float yVel)
    {
        xVel *= speed;
        yVel *= speed;
        base.Move(xVel, yVel);
    }
}
