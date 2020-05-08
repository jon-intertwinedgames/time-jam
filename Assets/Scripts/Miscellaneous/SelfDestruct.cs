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

    public void ResetWithNewTimer(float selfDestructionSeconds)
    {
        StopAllCoroutines();

        secondsToDestroySelf = selfDestructionSeconds;

        StartCoroutine(DestroySelf());
    }
}
