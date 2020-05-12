using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Fader))]
public class LoseScreen : MonoBehaviour
{
    Fader fader;

    // Start is called before the first frame update
    void Start()
    {
        fader = GetComponent<Fader>();
        GetComponent<CanvasGroup>().alpha = 0;
        GameObject.FindObjectOfType<GameMaster>().GameOver += Lose;
    }

    private void Lose(object sender, EventArgs e)
    {
        fader.FadeOut(1f);
    }
}
