using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected int startingHealth = 0;
    
    protected int health = 0;  //Remove SerializeField later

    protected virtual void Awake()
    {
        health = startingHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //Dies
        }
    }
}
