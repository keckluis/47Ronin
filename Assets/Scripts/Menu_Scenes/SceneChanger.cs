using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public SceneLoader SceneLoader;

    public bool GameOver = false;
    public bool NextScene = false;

    void Start()
    {
        if (GameObject.Find("SceneLoader"))
        {
            SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        }
    }
}
