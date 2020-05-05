using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Projectile : MonoBehaviour
{
    protected float rotationOffset;
    protected Movement movement_script;

    public static void CreateProjectile(GameObject projectileToCreate, Vector2 projectilePos, Vector2 projectileDirection, string projectileName)
    {
        GameObject newProjectile = Instantiate(projectileToCreate, projectilePos, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().SetInMotion(projectileDirection);
    }

    public void SetInMotion(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotationOffset;
        transform.Rotate(0, 0, -rotation);
        Vector2 vel = direction * movement_script.speed;
        movement_script.Move(vel);
    }
}
