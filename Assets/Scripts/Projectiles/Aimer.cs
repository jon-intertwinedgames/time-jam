﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.ComponentModel;
using System;

public class Aimer : MonoBehaviour
{
    [SerializeField] private GameObject[] projectile_GameObjects = new GameObject[0];

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
        lr.enabled = false;
    }

    public Vector2 GetDirectionOfAim(Vector2 targetPos)
    {
        return (targetPos - (Vector2)transform.position).normalized;
    }


    public void SetTrajectory(Vector2 targetPos)
    {
        Vector2 currentPosition = transform.position;
        Vector2 projectileDirection = GetDirectionOfAim(targetPos);

        projectileDirection *= 25;

        Vector3[] linePoints = { currentPosition, currentPosition + projectileDirection };
        lr.SetPositions(linePoints);
    }

    public void ShowTrajectory(bool shouldShow)
    {
        lr.enabled = shouldShow;
    }

    public Projectile ShootProjectile(Vector2 projectileDirection, string projectileName)
    {
        GameObject projectile = projectile_GameObjects.First(p => p.name.ToLower() == projectileName.ToLower());
        return Projectile.CreateProjectile(projectile, transform.position, projectileDirection);
    }

    public Projectile ShootProjectile(Vector2 projectileDirection, string projectileName, float speedPercentage)
    {
        GameObject projectile = projectile_GameObjects.First(p => p.name.ToLower() == projectileName.ToLower());
        return Projectile.CreateProjectile(projectile, transform.position, projectileDirection, speedPercentage);
    }

}
