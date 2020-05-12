using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : DamageCharacter
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<Health>().ChangeHealth(-damage);

            if (destroyColliderOnHit)
            {
                Destroy(GetComponent<BoxCollider2D>());
            }
        }
    }
}
