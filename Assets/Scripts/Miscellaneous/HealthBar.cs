using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Health))]
public class HealthBar : MonoBehaviour
{
    private Health health_script;

    [SerializeField] private RectTransform healthBar_trans = null;

    private float healthBarReductionRate, healthOffSet;

    private void Start()
    {
        health_script = GetComponent<Health>();

        healthBarReductionRate = healthBar_trans.rect.width / health_script.StartingHealth;
        healthOffSet = health_script.StartingHealth * healthBarReductionRate;

        UpdateHealth();
        health_script.DamageTakenEvent += UpdateHealth;
    }

    public void UpdateHealth()
    {
        Vector2 newHealthPos = healthBar_trans.localPosition;
        newHealthPos.x = health_script.CurrentHealth * healthBarReductionRate - healthOffSet;
        healthBar_trans.localPosition = newHealthPos;
    }
}