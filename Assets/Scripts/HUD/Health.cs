using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 0;
    public int StartingHealth { get => startingHealth; }

    private int currentHealth = 0;
    public int CurrentHealth { get => currentHealth; }

    public event Action HealthChangedEvent;                 //Executed when the character's health changes
    public event Action DamagedEvent;                       //Executed when the character is dealt damage
    public event Action RegenEvent;                         //Executed when the character gains health
    public event Action DeathEvent;                         //Executed when the character drops to 0 health or below                        

    protected virtual void Awake()
    {
        currentHealth = startingHealth;
    }

    [HideInInspector]
    public void ChangeHealth(int healthChange)              //Method to heal and damage character
    {
        currentHealth += healthChange;

        HealthChangedEvent?.Invoke();

        if(healthChange < 0)
        {
            DamagedEvent?.Invoke();

            if (currentHealth <= 0)
            {
                DeathEvent?.Invoke();
            }
        }

        else if(healthChange > 0)
        {
            RegenEvent?.Invoke();

            if (currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
    }
}
