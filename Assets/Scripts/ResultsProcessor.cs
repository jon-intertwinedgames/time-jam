using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsProcessor : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI howmanyKilled_text;

    // Start is called before the first frame update
    void Start()
    {
        howmanyKilled_text.text = GlobalVars.EnemiesKilled.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        howmanyKilled_text.text = GlobalVars.EnemiesKilled.ToString();
    }
}
