using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

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

    private Language Language;

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
            LoadingCanvas.SetActive(true);
            LoadingText.color = new Color(LoadingText.color.r, LoadingText.color.g, LoadingText.color.b, Mathf.PingPong(Time.time, 1));
        }

        if (Input.GetKeyDown(KeyCode.M) && !loadScene && NextScene != 2 && NextScene != 19)
        {
            if (MenuCanvas.activeSelf)
            {
                MenuCanvas.SetActive(false);
            }
            else
            {
                MenuCanvas.SetActive(true);
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
