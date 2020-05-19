using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    protected AirMovement movement_script;

    protected float rotationOffset;

    protected virtual void Awake()
    {
        movement_script = GetComponent<AirMovement>();
    }

    public static Projectile CreateProjectile(GameObject projectileToCreate, Vector2 projectilePos, Vector2 projectileDirection)
    {
        Projectile newProjectile_script = Instantiate(projectileToCreate, projectilePos, Quaternion.identity).GetComponent<Projectile>();
        newProjectile_script.SetInMotion(projectileDirection);
        return newProjectile_script;
    }

    public static Projectile CreateProjectile(GameObject projectileToCreate, Vector2 projectilePos, Vector2 projectileDirection, float speedpercentage)
    {
        Projectile newProjectile_script = Instantiate(projectileToCreate, projectilePos, Quaternion.identity).GetComponent<Projectile>();
        newProjectile_script.SetInMotion(projectileDirection, speedpercentage);
        return newProjectile_script;
    }

    protected void SetInMotion(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotationOffset;
        transform.Rotate(0, 0, -rotation);
        Vector2 vel = direction * movement_script.speed;
        movement_script.Move(vel);
    }

    protected void SetInMotion(Vector2 direction , float speedpercentage)
    {
        float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotationOffset;
        transform.Rotate(0, 0, -rotation);
        Vector2 vel = direction * movement_script.speed * speedpercentage;
        movement_script.Move(vel);
    }

}
