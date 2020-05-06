using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManipulation : MonoBehaviour
{
    private Coroutine timeLerp = null;

    [SerializeField]
    private float duration = 0;

    [Header("Time")]

    [SerializeField]
    [Range(0.1f, 0.75f)]
    private float targetTimeScaleSlow = 0;

    [Header("Black Screen")]

    [SerializeField]
    [Range(0.25f, 0.8f)]
    private float blackScreenAlphaValue = 0;

    [SerializeField]
    private Image blackScreen_image = null;

    public void StartSlowingDownTime()
    {
        timeLerp = StartCoroutine(LerpTime());
    }

    public void ResetTimeScale()
    {
        StopCoroutine(timeLerp);
        timeLerp = null;
        blackScreen_image.color = Color.clear;
        Time.timeScale = 1;
    }

    private IEnumerator LerpTime()
    {
        float timer = 0;
        while(Time.timeScale != targetTimeScaleSlow)
        {
            float currentTime = timer / duration;
            Time.timeScale = Mathf.Lerp(1, targetTimeScaleSlow, currentTime);
            blackScreen_image.color = Color.Lerp(Color.clear, new Color(0, 0, 0, blackScreenAlphaValue), currentTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}