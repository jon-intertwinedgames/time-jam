using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrifterSFX : MonoBehaviour
{
    private AudioSource audioSource = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
}
