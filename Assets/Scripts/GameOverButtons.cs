using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverButtons : MonoBehaviour
{
    private SceneLoader SceneLoader;
    private void Start()
    {
        SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

    }
    public void RestartLevel()
    {
        SceneLoader.LoadPreviousScene();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
