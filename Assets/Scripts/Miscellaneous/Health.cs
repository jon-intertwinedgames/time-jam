using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 0;
    public int StartingHealth { get => startingHealth; }

    private int currentHealth = 0;
    public int CurrentHealth { get => currentHealth; }

    protected virtual void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GetComponent<PlayerController>()?.UpdateHealth();   //Health Bar will update IF you are the player.

        if (currentHealth <= 0)
        {
            //Dies
        }
    }
}
