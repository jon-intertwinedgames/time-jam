using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Paused
}

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    public int numOfEnemiesToKillToWin = 2;

    private CanvasGroup pauseCanvasGroup;
    private static GameState state;
    public static GameState State { get => state; }

    [SerializeField]
    TextMeshProUGUI howmanyToKilled_text = null;

    public event EventHandler GameOver;
    private bool isGameOver = false;


    private void Awake()
    {
        SceneManager.sceneLoaded += OnNewLevelLoaded;
    }

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

        if(Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
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

    public void TogglePause()
    {        
        if (pauseCanvasGroup)
        {
            switch (state)
            {
                case GameState.Playing:
                    state = GameState.Paused;
                    Time.timeScale = 0 ;
                    pauseCanvasGroup.alpha = 1;
                    pauseCanvasGroup.interactable = true;                    
                    break;
                case GameState.Paused:
                    state = GameState.Playing;
                    Time.timeScale = 1;
                    pauseCanvasGroup.alpha = 0;
                    pauseCanvasGroup.interactable = false;

                    print("Made it     " + state + " " + Time.timeScale);
                    break;
            }   
        }
    }

    private void OnNewLevelLoaded(Scene newScene, LoadSceneMode sceneMode)
    {
        GameObject pauseScreen = GameObject.FindGameObjectWithTag("Pause Screen");
        pauseCanvasGroup = pauseScreen?.GetComponent<CanvasGroup>() ?? null;

        if (pauseCanvasGroup)
        {
            state = GameState.Paused;
            TogglePause();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            state = GameState.Playing;
            TogglePause();
        }
    }
}
