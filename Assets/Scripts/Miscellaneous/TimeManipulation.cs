using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManipulation : MonoBehaviour
{
    private Coroutine timeLerp = null;

    [SerializeField]
    private float secondsToSlow = 0;

    [Header("Time")]

    [SerializeField]
    private int startingTimeValue = 0;
    public int StartingTimeValue { get => startingTimeValue; }

    private float currentTimeValue;
    public float CurrentTimeValue { get => currentTimeValue; }

    [SerializeField]
    private int timeReductionPerSec = 0, timeRegenPerSec = 0;

    [SerializeField]
    [Range(0.1f, 0.75f)]
    private float slowScale = 0;

    [Header("Black Screen")]

    [SerializeField]
    [Range(0.25f, 0.8f)]
    private float blackScreenAlphaValue = 0;

    [SerializeField]
    private Image blackScreen_image = null;

    public event Action UpdateTimeBar;

    protected void Awake()
    {
        currentTimeValue = startingTimeValue;
    }

    private void Update()
    {
        if(Time.timeScale != 1 && GameMaster.State == GameState.Playing && UpdateTimeBar != null)
        {
            currentTimeValue -= timeReductionPerSec * Time.unscaledDeltaTime;

            if(currentTimeValue <= 0)
            {
                currentTimeValue = 0;
                ResetTimeScale();
            }
        }

        else if(Time.timeScale == 1)
        {
            currentTimeValue += timeRegenPerSec * Time.unscaledDeltaTime;

            if(currentTimeValue >= 100)
            {
                currentTimeValue = 100;
            }
        }

        UpdateTimeBar?.Invoke();
    }

    public void StartSlowingDownTime()
    {
        timeLerp = StartCoroutine(LerpTime());
    }

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        timeLerp = null;
        blackScreen_image.color = Color.clear;
        Time.timeScale = 1;
    }

    private IEnumerator LerpTime()
    {
        float timer = 0;
        while(Time.timeScale != slowScale)
        {
            float currentTime = timer / secondsToSlow;
            Time.timeScale = Mathf.Lerp(1, slowScale, currentTime);
            blackScreen_image.color = Color.Lerp(Color.clear, new Color(0, 0, 0, blackScreenAlphaValue), currentTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}