using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected int damage = 0;

    protected float rotationOffset;

    public static void CreateProjectile(GameObject projectileToCreate, Vector2 projectilePos, Vector2 projectileDirection, string projectileName)
    {
        GameObject newProjectile = Instantiate(projectileToCreate, projectilePos, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().SetInMotion(projectileDirection);
    }

    public abstract void SetInMotion(Vector2 direction);
}
