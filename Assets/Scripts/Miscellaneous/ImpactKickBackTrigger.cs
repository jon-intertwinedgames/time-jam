using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will make a triggers follow conservation of momentum if they hit an object
/// </summary>
//[RequireComponent(typeof(Rigidbody2D))]
public class ImpactKickBackTrigger : MonoBehaviour
{
    [SerializeField]
    private bool hitOnlyOnce = true;

    private bool hashit =false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitOnlyOnce && hashit) return;
        Rigidbody2D other = collision.gameObject.GetComponent<Rigidbody2D>();
        if (other)
        {
            try
            {
                
                Rigidbody2D my = GetComponent<Rigidbody2D>();
                print(my.velocity);
                other.velocity += (my.mass * my.velocity) / other.mass;
                //other.AddForce((my.mass * my.velocity) / other.mass);
                hashit = true;
            }
            catch (Exception ex)
            {
                print(ex);
                
            }
        }
    }
}
