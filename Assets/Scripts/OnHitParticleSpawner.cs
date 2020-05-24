using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitParticleSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnParticlebyTag[] spawnParticlebyTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var item in spawnParticlebyTag)
        {
            if(item.tag == collision.tag)
            {
                //ContactPoint2D[] points = new ContactPoint2D[10];
                //var length = GetComponent<Collider2D>().GetContacts(points);

                //for (int i = 0; i < length; i++)
                //{
                //    GameObject.Instantiate(item.prefab, points[i].point, Quaternion.identity);
                //}
                var p = GameObject.Instantiate(item.prefab, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(p.gameObject,5f);
            }
        }
    }

    [System.Serializable]
    private struct SpawnParticlebyTag {
        public string tag;
        public ParticleSystem prefab;
    }

}
