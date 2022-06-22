using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverButtons : MonoBehaviour
{
    private SceneLoader SceneLoader;
    public TextMeshProUGUI RestartButtonText;

    private void Start()
    {
        if (GameObject.Find("SceneLoader"))
            SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

        if (RestartButtonText != null)
            LookForLanguage();
    }
    public void RestartLevel()
    {
        SceneLoader.LoadPreviousScene();
    }

    public void BackToMenu()
    {
        if (GameObject.Find("SceneLoader"))
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().NextScene = 1;        
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();      
        }
    }

    public void LookForLanguage()
    {
        if (GameObject.Find("Language") && RestartButtonText != null)
        {
            Languages lang = GameObject.Find("Language").GetComponent<Language>().currentLanguage;

            if (lang == Languages.German)
                RestartButtonText.text = "LEVEL NEU STARTEN";
            else if (lang == Languages.English)
                RestartButtonText.text = "RESTART LEVEL";
        }
    }
}
