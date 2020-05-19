using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] public bool removeColliderOnHit = false;
    [SerializeField] protected int damage = 0;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Health>().ChangeHealth(-damage);

        if(removeColliderOnHit)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public int getDamage()
    {
        return damage;
    }
}
