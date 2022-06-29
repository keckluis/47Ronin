using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    private Language Language;
    private SceneLoader SceneLoader;
    public GameObject LangButtons;
    public GameObject LangENButton;
    public GameObject LangDEButton;
    public GameObject QuitButton;
    public Controls ActionMap;

    public GameObject Credits;
    public GameObject LevelSelect;

    private void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.UI.A.canceled += StartGame;
        ActionMap.UI.B.canceled += OpenCredits;
        
        ActionMap.UI.X.canceled += SwitchLanguage;
        ActionMap.UI.Y.canceled += OpenLevelSelect;
    }

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

        if (GameObject.Find("SceneLoader"))
        {
            SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        }
    }

    public void StartGame()
    {
        if (!Credits.activeSelf && !LevelSelect.activeSelf)
        {
            RemoveControls();
            SceneLoader.NextScene = 2;
            SceneLoader.LoadNextScene();
        }  
    }

    public void RemoveControls()
    {
        ActionMap.UI.A.started -= StartGame;
        ActionMap.UI.B.canceled -= CloseScreens;
        ActionMap.UI.B.canceled -= OpenCredits;
        ActionMap.UI.X.canceled -= SwitchLanguage;
        ActionMap.UI.Y.canceled -= OpenLevelSelect;
        ActionMap.Disable();
    }

    public void StartGame(InputAction.CallbackContext ctx)
    {
        StartGame();
    }

    public void OpenCredits(InputAction.CallbackContext ctx)
    {
        OpenCredits();
    }

    public void OpenLevelSelect(InputAction.CallbackContext ctx)
    {
        OpenLevelSelect();
    }

    public void CloseScreens(InputAction.CallbackContext ctx)
    {
        CloseScreens();
    }

    public void OpenCredits()
    {
        if (!Credits.activeSelf && !LevelSelect.activeSelf)
        {
            ActionMap.UI.B.canceled -= OpenCredits;
            ActionMap.UI.B.canceled += CloseScreens;
            LangButtons.SetActive(false);
            QuitButton.SetActive(false);
            Credits.SetActive(true);
        }
    }

    public void OpenLevelSelect()
    {
        if (!Credits.activeSelf && !LevelSelect.activeSelf)
        {
            ActionMap.UI.B.canceled -= OpenCredits;
            ActionMap.UI.B.canceled += CloseScreens;
            LangButtons.SetActive(false);
            QuitButton.SetActive(false);
            LevelSelect.SetActive(true);
        }
    }

    public void CloseScreens()
    {
        ActionMap.UI.B.canceled -= CloseScreens;
        ActionMap.UI.B.canceled += OpenCredits;
        
        Credits.SetActive(false);
        LevelSelect.SetActive(false);
        LangButtons.SetActive(true);

#if !UNITY_WEBGL
        QuitButton.SetActive(true);
#endif
    }

    public void SwitchLanguage(InputAction.CallbackContext ctx)
    {
        if(!Credits.activeSelf && !LevelSelect.activeSelf)
        {
            if (LangENButton.activeSelf)
            {
                SetLanguage("EN");
                LangENButton.SetActive(false);
                LangDEButton.SetActive(true);
            }
            else
            {
                SetLanguage("DE");
                LangENButton.SetActive(true);
                LangDEButton.SetActive(false);
            }
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
