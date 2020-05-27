using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ArrowSFX : MonoBehaviour
{
    AudioSource audioSource = null;

    [SerializeField]
    private AudioOptions arrowFlying, arrowHittingWall, arrowHittingFlesh;

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
        AudioManager.PlaySFX(audioSource, arrowFlying.Volume, arrowFlying.Delay, false, SFX.FlyingArrow);
    }

    /// <summary>
    /// SFX for when the arrow hits terrain
    /// </summary>
    private void ArrowHittingWallSFX()
    {
        AudioManager.PlayRandomSFX(audioSource, arrowHittingWall.Volume, arrowHittingWall.Delay, false, SFX.ArrowHittingWall2, SFX.ArrowHittingWall3, SFX.ArrowHittingWall4);
    }

    /// <summary>
    /// SFX for when the arrow hits flesh
    /// </summary>
    private void ArrowHittingFleshSFX()
    {
        AudioManager.PlayRandomSFX(audioSource, arrowHittingFlesh.Volume, arrowHittingFlesh.Delay, false, SFX.ArrowHittingFlesh1, SFX.ArrowHittingFlesh2, SFX.ArrowHittingFlesh3, SFX.ArrowHittingFlesh4);
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
