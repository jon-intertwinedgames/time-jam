using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Movement movement_script;

    public void SetInMotion(Vector2 direction)
    {
        Vector2 vel = direction * movement_script.speed;
        movement_script.Move(vel);
    }
}
