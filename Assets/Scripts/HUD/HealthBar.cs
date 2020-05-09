using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Health))]
public class HealthBar : HUDBar
{
    private Health health_script;

    protected override void Start()
    {
        health_script = GetComponent<Health>();
        startingValue = health_script.StartingHealth;

        base.Start();

        UpdateBar(startingValue);
        health_script.UpdateHealthBarEvent += UpdateBar;
    }
}