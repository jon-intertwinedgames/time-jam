using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerWrapper : MonoBehaviour
{
    public void LoadScene(int buildindex)
    {
        SceneManagement.LoadScene(buildindex);
    }

    public void LoadNextScene()
    {
        SceneManagement.LoadNextScene();
    }

    public void LoadRestartCurrentScene()
    {
        SceneManagement.LoadRestartCurrentScene();
    }
}
