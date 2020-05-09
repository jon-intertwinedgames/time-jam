using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] [Range(0, 2)]
    private float speedRatio = 0;

    private Vector2 offSet;

    private void Awake()
    {
        offSet = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = ((Vector2)Camera.main.transform.position - offSet) * speedRatio + offSet;
    }
}
