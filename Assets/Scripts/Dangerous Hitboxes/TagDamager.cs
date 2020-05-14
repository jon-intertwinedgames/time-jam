using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagDamager : Damager
{
    [SerializeField] private string tagName = "";

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagName)
        {
            base.OnTriggerEnter2D(collision);
        }        
    }
}
