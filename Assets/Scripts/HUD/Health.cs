using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 0;
    public int StartingHealth { get => startingHealth; }

    private int currentHealth = 0;
    public int CurrentHealth { get => currentHealth; }

    public event Action DeathEvent;
    public event Action<float> UpdateHealthBarEvent;

    protected virtual void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (UpdateHealthBarEvent != null)
        {
            UpdateHealthBarEvent(CurrentHealth);
        }

        if (currentHealth <= 0 && DeathEvent != null)
        {
            DeathEvent();
        }
    }
}
