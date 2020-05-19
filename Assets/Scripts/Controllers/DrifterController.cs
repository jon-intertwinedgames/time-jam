using com.leothelegion.Nav;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DrifterController : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    new ParticleSystem particleSystem = null;
    [SerializeField]
    List<Vector2> path;
    [SerializeField]
    Transform goal = null; // to make this run faster, we should make this static
    [SerializeField]
    float minFlyingAltitude = 10f;
    [SerializeField]
    [Range(0.00001f, 1f)]
    private float floatyness = 0.01f;
    [SerializeField]
    GameObject projectile = null;
    [SerializeField]
    float firingRange = 10f;
    [SerializeField]
    float firerate = 1f;
    [SerializeField]
    [Range(0.1F, 1.0F)]
    float shootingAccuracy = 1f;

    NavAgent agent = null;
    AirMovement movement = null;
    Health health = null;
    float time = 0f;
    Vector2 floatyMovement = Vector2.zero;
    float FlyingAltitude = 10f;

    System.Random ran = new System.Random();

    EmissionModule particleSystemEmission;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavAgent>();
        movement = GetComponent<AirMovement>();
        health = GetComponent<Health>();

        anim = GetComponentInChildren<Animator>();

        health.DeathEvent += Death;

        try
        {
            if (goal == null)
            {
                goal = GameObject.FindObjectOfType<PlayerController>().transform;
            }
        }
        catch(Exception ex)
        {

        }

        particleSystemEmission = particleSystem.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (goal == null) return;

        FlyingAltitude = goal.position.y + minFlyingAltitude;

        if (CanShootAtPlayer())
            ShootAtPlayer();

        particleSystemEmission.rateOverTimeMultiplier = 100f * (time / firerate);//<-- LOOK HERE Magic number :D
        print("");
    }

    private void FixedUpdate()
    {
        if (goal == null) return;
        HuntPlayer();
    }

    private bool CanShootAtPlayer()
    {
        if (Vector3.Distance(this.transform.position, goal.position) < firingRange)
        {
            time += Time.deltaTime;
            if ((time / firerate) < shootingAccuracy)
            {
                Targetbuffer.x = goal.position.x;
                Targetbuffer.y = goal.position.y;
                Targetbuffer.z = goal.position.z;
            }
            if (time > firerate)
            {
                time = ran.Next(0, 2);
                return true;
            }
        }
        else
        {
            time = 0;
        }
        return false;
    }

    Vector3 Targetbuffer;
    private void ShootAtPlayer()
    {
        Vector3 a = Targetbuffer;
        Vector3 b = this.transform.position;

        Projectile.CreateProjectile(projectile, this.transform.position, (Vector2)a - (Vector2)b);
    }
    
    private void HuntPlayer()
    {
        float xa = 0;
        float ya = 0;

        Vector2Int s = new Vector2Int(
            Mathf.FloorToInt(this.transform.position.x),
            Mathf.FloorToInt(this.transform.position.y)
            );

        Vector2Int e = new Vector2Int(
            Mathf.FloorToInt(goal.position.x),
            Mathf.FloorToInt(goal.position.y)
            );
        path.Clear();
        path.AddRange(agent.FindPath(s, e));
        
        if (path != null)
        {
            if (path.Count > 0)
            {
                var point = path[0];

                float x = this.transform.position.x;
                float y = this.transform.position.y;


                if (Vector2.Distance(point, this.transform.position) > 0.1f)
                {
                    if (point.x < x)
                        xa = -1;
                    else if ((point.x > x))
                        xa = 1;
                    else
                        xa = 0;

                    if (y > FlyingAltitude)
                    {
                        if (point.y < y)
                            ya = -1;
                        else if ((point.y > y))
                            ya = 1;
                        else
                            ya = 0;
                    }
                    else if (y < FlyingAltitude)
                    {
                        ya = 1;
                    }
                    else if (y == FlyingAltitude)
                    {
                        ya = 0;
                    }


                }
            }
        }

        floatyMovement = Vector2.Lerp(floatyMovement, new Vector2(xa, ya), floatyness);

        movement.Move(new Vector2(floatyMovement.x, floatyMovement.y));
    }

    private void Death()
    {
        GlobalVars.EnemiesKilled += 1;
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Projectile"))
        {
            anim.SetTrigger("Damaged");
        }
    }

    //Called by Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Targetbuffer, 1f);
        ///
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, firingRange);
        /////////
        Gizmos.color = Color.yellow;
        var drawRange = 10;
        Gizmos.DrawLine(
            new Vector3(this.transform.position.x - drawRange, FlyingAltitude, 0),
            new Vector3(this.transform.position.x + drawRange, FlyingAltitude, 0)
            );
        ///////////////
        if (path == null) return;
        Gizmos.color = Color.green;
        foreach (var p in path)
        {
           Gizmos.DrawWireSphere(new Vector2(p.x, p.y), 0.1f);
        }
    }

}
