using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    int numOfEnemiesToKillToWin = 2;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVars.EnemiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVars.EnemiesKilled >= numOfEnemiesToKillToWin)
            GetComponent<SceneManagerWrapper>().LoadNextScene();
    }
}
