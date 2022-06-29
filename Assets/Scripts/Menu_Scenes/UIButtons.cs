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
   
    [SerializeField]
    private bool isMenuMoving = false;
    [SerializeField]
    private bool moveIn = true;
    public int MenuOut;
    public int MenuIn;
    static float t = 0.0f;


    private void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.UI.Menu.performed += OpenCloseMenu;
        ActionMap.UI.X.performed += SkipLevel;
        ActionMap.UI.B.performed += BackToMenu;
        ActionMap.UI.Y.performed += RestartLevel;
    }

    private void Start()
    {
        if (GameObject.Find("SceneLoader"))
            SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();

        if (RestartButtonText != null)
            LookForLanguage();
    }

    private void Update()
    {
        if (SceneLoader.loadScene)
        {
            isMenuMoving = false;
            moveIn = true;
            Menu.transform.localPosition = new Vector3(MenuOut, Menu.transform.localPosition.y, Menu.transform.localPosition.z);
        }
            if (isMenuMoving && moveIn)
        {
            Menu.SetActive(true);
            Menu.transform.localPosition = new Vector3(Mathf.Lerp(Menu.transform.localPosition.x, MenuIn, t), -530, 0);
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f || Menu.transform.localPosition.x == MenuIn)
            {
                t = 0.0f;
                isMenuMoving = false;
            }
        }
        else if (isMenuMoving && !moveIn)
        {
            Menu.SetActive(true);
            Menu.transform.localPosition = new Vector3(Mathf.Lerp(Menu.transform.localPosition.x, MenuOut, t), -530, 0);
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f || Menu.transform.localPosition.x == MenuOut)
            {
                t = 0.0f;
                isMenuMoving = false;
            }
        }
        if (!isMenuMoving && !moveIn)
        {
            Menu.SetActive(false);
            moveIn = true;
        }

        if (RestartButtonText != null)
            LookForLanguage();
    }

    public void OpenCloseMenu(InputAction.CallbackContext context)
    {
        OpenCloseMenu();
    }

    public void OpenCloseMenu()
    {
        if (!SceneLoader.loadScene && SceneLoader.NextScene != 2 && SceneLoader.NextScene != 19 && SceneLoader.NextScene != 20 && !isMenuMoving)
        {
            if (!Menu.activeSelf)
            {
                isMenuMoving = true;
                moveIn = true;
            }
            else
            {
                isMenuMoving = true;
                moveIn = false;
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
        }
    }
    public void BackToMenu(InputAction.CallbackContext context)
    {
        BackToMenu();
    }

    public void SkipLevel()
    {
        if (SceneLoader != null && Menu.activeSelf)
        {    
            SceneLoader.LoadNextScene();
            Menu.SetActive(false);
        }
    }

    public void SkipLevel(InputAction.CallbackContext context)
    {
        if (SceneLoader != null && Menu.activeSelf)
        {
            SceneLoader.LoadNextScene();
            Menu.SetActive(false);
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
