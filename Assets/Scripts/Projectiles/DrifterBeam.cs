using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterBeam: MonoBehaviour
{
    [SerializeField] private int damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(GetComponent<BoxCollider2D>());
        }
        
    }
}
