using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Rigidbody2D rb;
    
    [SerializeField] private float defaultSpeed = 0;
    [HideInInspector] public float speed;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = defaultSpeed;
    }

    protected void Move(Vector2 vel)
    {
        Move(vel.x, vel.y);
    }

    protected void Move(float xVel, float yVel)
    {
        Vector2 forcetoAdd = new Vector2(xVel, yVel);

        if ((rb.velocity.x > defaultSpeed && xVel > 0) || (rb.velocity.x < -defaultSpeed && xVel < 0))
            return;

        if (rb.velocity.magnitude != 0)
        {
            forcetoAdd -= rb.velocity;

            if (rb.velocity.y == 0)
            {
                if ((rb.velocity.x > 0 && xVel <= 0) || (rb.velocity.x < 0 && xVel >= 0))
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }

        rb.AddForce(forcetoAdd);
        //rb.velocity = new Vector2(xVel, yVel);
    }
}
