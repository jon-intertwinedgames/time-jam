﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirMovement), typeof(Rigidbody2D))]
public class Arrow : Projectile
{
    protected AirMovement movement_script;

    private Rigidbody2D rb;

    [SerializeField] [Range(0.01f, 0.2f)]
    private float slowDownDuration = 0;
    private bool struckSomething = false;

    private void Awake()
    {
        movement_script = GetComponent<AirMovement>();
        rb = GetComponent<Rigidbody2D>();
        transform.SetParent(GetArrowContainer());
        rotationOffset = -90;
    }

    public override void SetInMotion(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotationOffset;
        transform.Rotate(0, 0, -rotation);
        Vector2 vel = direction * movement_script.speed;
        movement_script.Move(vel);
    }

    public static Transform GetArrowContainer()
    {
        return GameObject.FindGameObjectWithTag("Arrow Container").transform;
    }

    public static Transform[] GetAllActiveArrows()
    {
        Transform arrowContainer_trans = Arrow.GetArrowContainer();
        int numOfArrows = arrowContainer_trans.childCount;
        Transform[] allArrows = new Transform[numOfArrows];

        for (int i = 0; i < numOfArrows; i++)
            allArrows[i] = arrowContainer_trans.GetChild(i);

        return allArrows;
    }
//d
    public static GameObject FindClosestArrowToCursor(Transform[] allArrows)
    {
        Vector2 mouseInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject closestArrow = allArrows[0].gameObject;
        float shortestDistance = ((Vector2)closestArrow.transform.position - mouseInWorldPoint).magnitude;

        for (int i = 1; i < allArrows.Length; i++)
        {
            Transform currentArrow = allArrows[i];
            Vector2 distance = (Vector2)currentArrow.position - mouseInWorldPoint;
            float distanceMagnitude = distance.magnitude;

            if (distanceMagnitude < shortestDistance)
            {
                shortestDistance = distanceMagnitude;
                closestArrow = currentArrow.gameObject;
            }
        }

        return closestArrow;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (struckSomething == false)
            StartCoroutine(SlowDown());
    }

    private IEnumerator SlowDown()
    {
        struckSomething = true;

        float timer = 0;
        Vector2 currentSpeed = rb.velocity;
        while (rb.velocity != Vector2.zero)
        {
            rb.velocity = Vector2.Lerp(currentSpeed, Vector2.zero, timer/slowDownDuration);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}