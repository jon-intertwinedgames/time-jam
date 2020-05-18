using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsProcessor : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI howmanyKilled_text = null;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateText()
    {
        howmanyKilled_text.text = GlobalVars.EnemiesKilled.ToString();
    }
}
