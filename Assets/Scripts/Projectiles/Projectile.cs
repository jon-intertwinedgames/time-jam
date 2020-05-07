using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    protected float rotationOffset;

    public static void CreateProjectile(GameObject projectileToCreate, Vector2 projectilePos, Vector2 projectileDirection, string projectileName)
    {
        GameObject newProjectile = Instantiate(projectileToCreate, projectilePos, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().SetInMotion(projectileDirection);
    }

    public static void CreateProjectile(GameObject projectileToCreate, Vector2 projectilePos, Quaternion projectilerot)
    {
        var newProjectile = Instantiate(projectileToCreate, projectilePos, projectilerot);

        var toEuler = projectilerot.eulerAngles;

        var xa = Mathf.Cos(toEuler.z * (Mathf.PI / 180));
        var ya = Mathf.Sin(toEuler.z * (Mathf.PI / 180));

        newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(xa,ya)*5f;

        Destroy(newProjectile, 5f);
    }

    public abstract void SetInMotion(Vector2 direction);
}
