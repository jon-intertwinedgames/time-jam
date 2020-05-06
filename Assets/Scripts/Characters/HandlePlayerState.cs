using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerState : MonoBehaviour
{
    Rigidbody2D rb;

    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Soaring
    }

    private PlayerState actionState;
    public PlayerState ActionState
    {
        get => actionState;

        set
        {
            if (actionState != value)
            {
                actionState = value;

                switch (actionState)
                {
                    case PlayerState.Idle:
                        break;
                    case PlayerState.Running:
                        break;
                    case PlayerState.Jumping:
                        break;
                    case PlayerState.Falling:
                        break;
                    case PlayerState.Soaring:
                        break;
                }
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (rb.velocity == Vector2.zero)
            ActionState = PlayerState.Idle;
        else if (rb.velocity.x != 0 && rb.velocity.y == 0)
            ActionState = PlayerState.Running;
        else if (rb.velocity.y > 0 && actionState != PlayerState.Soaring)
            ActionState = PlayerState.Jumping;
        else if (rb.velocity.y < 0 && actionState != PlayerState.Soaring)
            ActionState = PlayerState.Falling;
    }
}
