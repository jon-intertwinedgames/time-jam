using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class RotateBasedOnVelocity : MonoBehaviour
{
    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbody == null) return;
        if (rigidbody.velocity == Vector2.zero) return;
        var rot2 = Quaternion.LookRotation(rigidbody.velocity);

        rot2.x = 0f;
        rot2.y = 0f;

        transform.rotation = rot2;
    }
}
