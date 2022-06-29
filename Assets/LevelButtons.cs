using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    private SceneLoader sceneLoader;
    public MainMenuButtons MMButtons;
    void Start()
    {
        if (GameObject.Find("SceneLoader"))
        {
            sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        }
    }

    public void LoadScence(int index)
    {
        if (sceneLoader != null)
        {
            MMButtons.RemoveControls();
            sceneLoader.LoadSpecificScene(index);
        }
    }
}
