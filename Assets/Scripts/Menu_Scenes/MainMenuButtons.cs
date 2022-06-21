using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    private Language Language;
    public GameObject LangENButton;
    public GameObject LangDEButton;

    private void Start()
    {
        if (GameObject.Find("Language"))
        {
            Language = GameObject.Find("Language").GetComponent<Language>();
            
            if (Language.currentLanguage == Languages.German)
            {
                LangENButton.SetActive(true);
                LangDEButton.SetActive(false);
            }
            else if (Language.currentLanguage == Languages.English)
            {
                LangENButton.SetActive(false);
                LangDEButton.SetActive(true);
            }
        }
    }

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
        if (Language != null)
        {
            Language.SetLanguage(lang);
        }
    }
}
