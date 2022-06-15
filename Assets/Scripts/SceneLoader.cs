using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{

    private bool loadScene = false;

    public int NextScene;
    public int PreviousScene;

    [SerializeField]
    private TextMeshProUGUI LoadingText;
    [SerializeField]
    private GameObject Canvas;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

#if (UNITY_EDITOR)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LoadGameOver();
        }
    }
#endif

    public void LoadNextScene()
    {
        if (loadScene == false)
        {
            loadScene = true;
            StartCoroutine(LoadNewScene());
        }

        if (loadScene == true)
        {
            Canvas.SetActive(true);
            LoadingText.color = new Color(LoadingText.color.r, LoadingText.color.g, LoadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

    public void LoadPreviousScene()
    {
        NextScene = PreviousScene;
        LoadNextScene();
    }

    public void LoadGameOver()
    {
        PreviousScene = NextScene - 1;
        NextScene = 17;
        LoadNextScene();
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);
        
        AsyncOperation async = SceneManager.LoadSceneAsync(NextScene);

        while (!async.isDone)
        {
            yield return null;
        }
        Canvas.SetActive(false);
        loadScene = false;
        NextScene += 1;
    }
}
