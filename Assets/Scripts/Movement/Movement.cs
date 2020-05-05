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

    public virtual void Move(Vector2 vel)
    {
        Move(vel.x, vel.y);
    }

    public virtual void Move(float xVel, float yVel)
    {
        rb.velocity = new Vector2(xVel, yVel);
    }
}
