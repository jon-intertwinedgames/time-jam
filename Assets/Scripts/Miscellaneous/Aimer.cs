using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.ComponentModel;
using System;

public class Aimer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] projectile_GameObjects = new GameObject[0];

    public Vector2 GetDirection(Vector2 originPos, Vector2 targetPos)
    {
        return (targetPos - originPos).normalized;
    }

    public void CreateProjectile(Vector2 projectilePos, Vector2 projectileDirection, string projectileName)
    {
        GameObject projectile = projectile_GameObjects.First(p => p.name.ToLower() == projectileName.ToLower());

        GameObject newProjectile = Instantiate(projectile, projectilePos, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().SetInMotion(projectileDirection);
    }
}
