using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAccumulator : MonoBehaviour
{
    private static Text enemiesKilled_text;
    private static int score;

    void Awake()
    {
        enemiesKilled_text = GetComponent<Text>();
        enemiesKilled_text.text = score.ToString();
    }

    public static void AddToScore(int pointsAdded)
    {
        score += pointsAdded;
        enemiesKilled_text.text = score.ToString();
    }
}
