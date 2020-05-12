﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterBeam: Projectile
{
    protected override void Awake()
    {
        base.Awake();
        rotationOffset = -90;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<Health>().ChangeHealth(-damage);
        }
    }
}
