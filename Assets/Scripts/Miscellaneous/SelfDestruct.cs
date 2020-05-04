using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float secondsToDestroySelf = 0;

    void Awake()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(secondsToDestroySelf);
        Destroy(gameObject);
    }
}
