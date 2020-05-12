using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterBeam: Projectile
{
    protected override void Awake()
    {
        base.Awake();
        rotationOffset = -90;
    }
}
