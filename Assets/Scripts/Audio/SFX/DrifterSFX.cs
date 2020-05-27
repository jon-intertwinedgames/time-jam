using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterSFX : MonoBehaviour
{
    [SerializeField]
    private AudioOptions drifting, death;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        DriftingSFX();
        GetComponent<Health>().DeathEvent += DeathSFX;
    }

    private void DriftingSFX()
    {
        AudioManager.PlaySFX(audioSource, drifting.Volume, drifting.Delay, true, SFX.Drifting);
    }

    private void DeathSFX()
    {
        AudioManager.PlayOneShotSFX(audioSource, death.Volume, death.Delay, SFX.DrifterDeath);
    }
}
