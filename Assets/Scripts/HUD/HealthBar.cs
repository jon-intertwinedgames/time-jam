using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent (typeof(Health))]
public class HealthBar : HUDBar
{
    private Health health_script;

    [SerializeField]
    private bool showHealthText;

    [SerializeField]
    private TextMeshProUGUI currentHealth_text;

    protected override void Start()
    {
        health_script = GetComponent<Health>();
        startingValue = health_script.StartingHealth;

        base.Start();

        UpdateBar(startingValue);
        health_script.HealthChangedEvent += delegate { UpdateBar(health_script.CurrentHealth); };

        if(showHealthText)
        {
            health_script.HealthChangedEvent += UpdateHealthText;
        }
    }

    private void UpdateHealthText()
    {
        currentHealth_text.text = health_script.CurrentHealth.ToString();
    }
}