using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private RectTransform healthBar_trans = null;

    private float healthBarReductionRate, healthOffSet;

    protected override void Awake()
    {
        base.Awake();
        healthBarReductionRate = healthBar_trans.rect.width / startingHealth;
        healthOffSet = startingHealth * healthBarReductionRate;
    }

    public int Health
    {
        get => health;

        set
        {
            if (health != value)
            {
                health = value;

                UpdateHealth();
            }
        }
    }

    private void UpdateHealth()
    {
        Vector2 newHealthPos = healthBar_trans.localPosition;
        newHealthPos.x = health * healthBarReductionRate - healthOffSet;
        healthBar_trans.localPosition = newHealthPos;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            //Dies
        }
    }
}
