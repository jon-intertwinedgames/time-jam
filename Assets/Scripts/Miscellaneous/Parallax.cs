using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] [Range (0, 2)]
    private float xParallaxEffect = 0, yParallaxEffect = 0;

    private Transform camera_trans;
    private Vector2 spriteStartPosOffset, cameraStartPosOffset;

    private void Awake()
    {
        camera_trans = Camera.main.transform;
        cameraStartPosOffset = camera_trans.position;
        spriteStartPosOffset = transform.position;
    }

    private void LateUpdate()
    {
        Vector2 parallaxEffect = new Vector2(xParallaxEffect, yParallaxEffect);
        Vector2 cameraPos = camera_trans.position;

        Vector2 distanceToTravel = (cameraStartPosOffset - cameraPos) * parallaxEffect;
        transform.position = spriteStartPosOffset - distanceToTravel;
    }
}
