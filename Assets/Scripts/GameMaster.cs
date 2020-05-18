using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    public int numOfEnemiesToKillToWin = 2;


    [SerializeField]
    TextMeshProUGUI howmanyToKilled_text = null;

    public event EventHandler GameOver;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVars.EnemiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (howmanyToKilled_text != null)
            howmanyToKilled_text.text = numOfEnemiesToKillToWin.ToString();

        CheckforWin();
        CheckforLose();
    }

    private void CheckforLose()
    {
        if (GameObject.FindObjectOfType<PlayerController>() == null)
        {
            if (!isGameOver)
            {
                OnGameOver(EventArgs.Empty);
                print("GAMEOVER....");
                isGameOver = true;
            }

        }
    }

    private void CheckforWin()
    {
        try
        {
            if (GlobalVars.EnemiesKilled >= numOfEnemiesToKillToWin)
                GetComponent<SceneManagerWrapper>().LoadNextScene();
        }
        catch (System.Exception)
        {
            print("I'm not in the build.... I can't go to next scene");
        }
    }

    private void OnGameOver(EventArgs e)
    {
        EventHandler handler = GameOver;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}
