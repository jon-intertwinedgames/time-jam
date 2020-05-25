using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SFXOptions
{
    [SerializeField][Range(0, 1)]
    private float volume;

    [SerializeField]
    private float delay;

    public Coroutine sfxCoroutine;

    public float Volume { get => volume; }
    public float Delay { get => delay; }

    public SFXOptions(float volume, float delay, Coroutine sfxCoroutine)
    {
        this.volume = volume;
        this.delay = delay;
        this.sfxCoroutine = sfxCoroutine;
    }
}
