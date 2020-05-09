using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    static SceneManagement INSTANCE;

    // Start is called before the first frame update
    void Start()
    {
        INSTANCE = this;
    }

    public static void LoadScene( int buildindex)
    {
        SceneManager.LoadScene(buildindex);
    }

    public static void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadRestartCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
