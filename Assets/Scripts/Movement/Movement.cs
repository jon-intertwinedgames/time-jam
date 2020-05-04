using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Rigidbody2D rb;
    
    [SerializeField] private float defaultSpeed = 0;
    [HideInInspector] public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = defaultSpeed;
    }

    public virtual void Move(Vector2 vel)
    {
        rb.velocity = vel;
    }

    public virtual void Move(float xVel, float yVel)
    {
        rb.velocity = new Vector2(xVel, yVel);
    }
}
