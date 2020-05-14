﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ArrowSFX : MonoBehaviour
{
    AudioSource audioSource = null;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        ArrowFlyingSFX();
    }

    /// <summary>
    /// SFX for arrow being released from the bow
    /// </summary>
    private void ArrowFlyingSFX()
    {
        AudioManager.PlayRandomSFXOneShot(audioSource, SFX.FlyingArrow1, SFX.FlyingArrow2, SFX.FlyingArrow3, SFX.FlyingArrow4);
    }

    /// <summary>
    /// SFX for when the arrow hits terrain
    /// </summary>
    private void ArrowHittingWallSFX()
    {
        AudioManager.PlayRandomSFXOneShot(audioSource, SFX.ArrowHittingWall2, SFX.ArrowHittingWall3, SFX.ArrowHittingWall4);
    }

    /// <summary>
    /// SFX for when the arrow hits flesh
    /// </summary>
    private void ArrowHittingFleshSFX()
    {
        AudioManager.PlayOneShotSFX(audioSource, SFX.ArrowHittingFlesh1);
    }

    /// <summary>
    /// Collision/Impact SFX
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.StopPlaying(audioSource);

        string collidedLayer = LayerMask.LayerToName(collision.gameObject.layer);

        if (collidedLayer == "Terrain")
        {
            ArrowHittingWallSFX();
        }

        else if (collidedLayer == "Enemies")
        {
            ArrowHittingFleshSFX();
        }
    }
}