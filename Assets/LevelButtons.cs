using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    private SceneLoader sceneLoader;
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
            sceneLoader.LoadSpecificScene(index);
    }
}
