using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirMovement), typeof(Rigidbody2D))]
public class Arrow : Projectile
{
    private void Awake()
    {
        movement_script = GetComponent<AirMovement>();
    }
}