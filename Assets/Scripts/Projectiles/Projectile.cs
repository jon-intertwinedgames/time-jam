using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected int damage = 0;

    protected AirMovement movement_script;

    protected float rotationOffset;

    protected virtual void Awake()
    {
        movement_script = GetComponent<AirMovement>();
    }

    public static void CreateProjectile(GameObject projectileToCreate, Vector2 projectilePos, Vector2 projectileDirection)
    {
        Projectile newProjectile_script = Instantiate(projectileToCreate, projectilePos, Quaternion.identity).GetComponent<Projectile>();
        newProjectile_script.SetInMotion(projectileDirection);
    }

    //public static void CreateProjectile(GameObject projectileToCreate, Vector2 projectilePos, Quaternion projectilerot)
    //{
    //    var newProjectile = Instantiate(projectileToCreate, projectilePos, projectilerot);

    //    var toEuler = projectilerot.eulerAngles;

    //    var xa = Mathf.Cos(toEuler.z * (Mathf.PI / 180));
    //    var ya = Mathf.Sin(toEuler.z * (Mathf.PI / 180));

    //    newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(xa,ya)*5f;

    //    Destroy(newProjectile, 5f);
    //}

    protected void SetInMotion(Vector2 direction)
    {
        float rotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + rotationOffset;
        transform.Rotate(0, 0, -rotation);
        Vector2 vel = direction * movement_script.speed;
        movement_script.Move(vel);
    }
}
