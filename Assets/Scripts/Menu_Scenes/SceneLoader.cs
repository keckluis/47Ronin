using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{

    private bool loadScene = false;

    public int NextScene  = 1;
    public int PreviousScene = -1;

    [SerializeField]
    private TextMeshProUGUI LoadingText;
    [SerializeField]
    private GameObject LoadingCanvas;
    [SerializeField]
    private GameObject MenuCanvas;
    [SerializeField]
    private bool isMenuMoving = false;
    [SerializeField]
    private bool moveIn = true;
    public int MenuOut;
    public int MenuIn;
    static float t = 0.0f;

    private Language Language;

    public Controls ActionMap;

    private void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Menu.menu.performed += Menu;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (GameObject.Find("Language"))
            Language = GameObject.Find("Language").GetComponent<Language>();

        LoadNextScene();
    }


    private void Update()
    {
        if (Language != null)
        {
            if (Language.currentLanguage == Languages.German)
                LoadingText.text = "LÄDT...";
            else if (Language.currentLanguage == Languages.English)
                LoadingText.text = "LOADING...";
        }

        if (loadScene)
        {
            isMenuMoving = false;
            moveIn = true;
            MenuCanvas.transform.localPosition = new Vector3(MenuOut, MenuCanvas.transform.localPosition.y, MenuCanvas.transform.localPosition.z);
            LoadingCanvas.SetActive(true);
            LoadingText.color = new Color(LoadingText.color.r, LoadingText.color.g, LoadingText.color.b, Mathf.PingPong(Time.time, 1));
        }

        if (isMenuMoving && moveIn)
        {
            MenuCanvas.SetActive(true);
            MenuCanvas.transform.localPosition = new Vector3(Mathf.Lerp(MenuCanvas.transform.localPosition.x, MenuIn, t), -539.938f, 0);
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f || MenuCanvas.transform.localPosition.x == MenuIn)
            {
                t = 0.0f;
                isMenuMoving = false;
            }
        }
        else if (isMenuMoving && !moveIn)
        {
            MenuCanvas.SetActive(true);
            MenuCanvas.transform.localPosition = new Vector3(Mathf.Lerp(MenuCanvas.transform.localPosition.x, MenuOut, t), -539.938f, 0);
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f || MenuCanvas.transform.localPosition.x == MenuOut)
            {
                t = 0.0f;
                isMenuMoving = false;
            }
        }
        if (!isMenuMoving && !moveIn)
        {
            MenuCanvas.SetActive(false);
            moveIn = true;
        }
    }

    public void Menu(InputAction.CallbackContext context)
    {
        if (!loadScene && NextScene != 2 && NextScene != 19 && !isMenuMoving)
        {
            if (!MenuCanvas.activeSelf)
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

    public void LoadNextScene()
    {
        if (!loadScene)
        {
            loadScene = true;
            StartCoroutine(LoadNewScene(NextScene));
            NextScene += 1;
        }
    }

    public void LoadPreviousScene()
    {
        if (!loadScene)
        {
            loadScene = true;
            StartCoroutine(LoadNewScene(PreviousScene));
            NextScene = PreviousScene + 1;
        }
    }

    public void LoadGameOver()
    {
        PreviousScene = NextScene - 1;
        NextScene = 18;
        LoadNextScene();
    }

    IEnumerator LoadNewScene(int SceneIndex)
    {
        yield return new WaitForSeconds(3);
        
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneIndex);

        while (!async.isDone)
        {
            yield return null;
        }
        LoadingCanvas.SetActive(false);
        loadScene = false;
    }
}
