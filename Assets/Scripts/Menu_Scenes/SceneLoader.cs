using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{

    public bool Loading = false;

    public int NextScene  = 1;
    public int PreviousScene = -1;

    [SerializeField]
    private TextMeshProUGUI LoadingText;
    [SerializeField]
    private GameObject LoadingCanvas;

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

        if (Loading)
        {
            LoadingCanvas.SetActive(true);
            LoadingText.color = new Color(LoadingText.color.r, LoadingText.color.g, LoadingText.color.b, Mathf.PingPong(Time.time, 1));
        }

        
    }

    public void LoadNextScene()
    {
        if (!Loading)
        {
            Loading = true;
            StartCoroutine(LoadNewScene(NextScene));
            NextScene += 1;
        }
    }

    public void LoadPreviousScene()
    {
        if (!Loading)
        {
            Loading = true;
            StartCoroutine(LoadNewScene(PreviousScene));
            NextScene = PreviousScene + 1;
        }
    }

    public void LoadGameOver()
    {
        PreviousScene = NextScene - 1;
        NextScene = SceneManager.sceneCountInBuildSettings - 1;
        LoadNextScene();
    }

    public void LoadSpecificScene(int index)
    {
        NextScene = index;
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
        Loading = false;
    }
}
