using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private bool removeColliderOnHit;
    [SerializeField] protected int damage;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Health>().ChangeHealth(-damage);

        if(removeColliderOnHit)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
