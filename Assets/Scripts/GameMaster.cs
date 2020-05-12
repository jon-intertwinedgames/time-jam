using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    int numOfEnemiesToKillToWin = 2;

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
        try
        {
            if (GlobalVars.EnemiesKilled >= numOfEnemiesToKillToWin)
                GetComponent<SceneManagerWrapper>().LoadNextScene();
        }
        catch (System.Exception)
        {
            print("I'm not in the build.... I can't go to next scene");
        }
        
        if(GameObject.FindObjectOfType<PlayerController>() == null)
        {
            if (!isGameOver)
            {
                OnGameOver(EventArgs.Empty);
                print("GAMEOVER....");
                isGameOver = true;
            }
                
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
