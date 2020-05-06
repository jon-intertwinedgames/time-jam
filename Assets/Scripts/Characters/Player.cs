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
        UpdateHealth();

        healthBarReductionRate = healthBar_trans.rect.width / startingHealth;
        healthOffSet = startingHealth * healthBarReductionRate;
    }

    private void UpdateHealth()
    {
        Vector2 newHealthPos = healthBar_trans.localPosition;
        newHealthPos.x = health * healthBarReductionRate - healthOffSet;
        healthBar_trans.localPosition = newHealthPos;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        UpdateHealth();
    }
}
