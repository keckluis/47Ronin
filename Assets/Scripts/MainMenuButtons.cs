using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        if (GameObject.Find("SceneLoader"))
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().NextScene = 2;
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();
        }
    }

    public void SetLanguage(string lang)
    {
        if (GameObject.Find("Language"))
        {
            GameObject.Find("Language").GetComponent<Language>().SetLanguage(lang);
        }
    }
}
