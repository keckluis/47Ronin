using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class UIButtons : MonoBehaviour
{
    private SceneLoader SceneLoader;
    public TextMeshProUGUI RestartButtonText;
    public GameObject Menu;

    public Controls ActionMap;
  
    private Language Lang;

    private void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.UI.Start.performed += OpenMenu;
        ActionMap.UI.B.performed += CloseMenu;
        ActionMap.UI.A.performed += BackToMenu;
        ActionMap.UI.Y.performed += RestartLevel;
    }

    private void Start()
    {
        if (GameObject.Find("SceneLoader"))
            SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

        if (RestartButtonText != null)
            LookForLanguage();

        if (GameObject.Find("Language"))
            Lang = GameObject.Find("Language").GetComponent<Language>();
    }

    private void FixedUpdate()
    {
        if (SceneLoader.Loading)
        {     
            CloseMenu();
        }

        LookForLanguage();
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        OpenMenu();
    }
    public void CloseMenu(InputAction.CallbackContext context)
    {
        CloseMenu();
    }

    public void OpenMenu()
    {
        if (!SceneLoader.Loading && SceneLoader.NextScene != 2 && SceneLoader.NextScene != 19 && SceneLoader.NextScene != 20)
        {
            if (!Menu.activeSelf)
            {
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void CloseMenu()
    {
        if (!SceneLoader.Loading && SceneLoader.NextScene != 2 && SceneLoader.NextScene != 19 && SceneLoader.NextScene != 20)
        {
            if (Menu.activeSelf)
            {
                Menu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void RestartLevel()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
            SceneLoader.LoadPreviousScene();
     
        RestartButtonText = null;
    }
    public void RestartLevel(InputAction.CallbackContext context)
    {
        RestartLevel();
    }

    public void BackToMenu()
    {
        if (Menu.activeSelf || SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().name == "End")
        {
            SceneLoader.NextScene = 1;
            SceneLoader.LoadNextScene();
            Menu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void BackToMenu(InputAction.CallbackContext context)
    {
        BackToMenu();
    }

    /*public void SkipLevel()
    {
        if (SceneLoader != null && Menu.activeSelf)
        {    
            SceneLoader.LoadNextScene();
            Menu.SetActive(false);
        }
    }*/

    public void LookForLanguage()
    {
        if (Lang != null)
        {
            Languages lang = Lang.currentLanguage;

            if (lang == Languages.German)
            {
                if (RestartButtonText != null)
                    RestartButtonText.text = "LEVEL NEU STARTEN";
            }          
            else if (lang == Languages.English)
            {
                if (RestartButtonText != null)
                    RestartButtonText.text = "RESTART LEVEL";
            }               
        }
    }
}
