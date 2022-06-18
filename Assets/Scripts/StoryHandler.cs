using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.Networking;

public class StoryHandler : MonoBehaviour
{
    public Transform Camera;
    public float Y = 0;
    public float Z = 0;

    public List<float> Positions;

    public string TextFileName;
    public TextMeshProUGUI Text;

    [SerializeField]private int currentPos = 0;
    private bool isMoving = false;
    static float t = 0.0f;

    private List<string> Texts;

    public GameObject ControlsRight;

    private string json;
    private bool gettingTexts = false;

    private void Start()
    {
        GetTexts(TextFileName);
    }

    private void Update()
    {
        if (Texts == null && !gettingTexts)
        {
            GetTexts(TextFileName);
        }

        if (isMoving)
        {
            Camera.localPosition = new Vector3(Mathf.Lerp(Camera.localPosition.x, Positions[currentPos], t), Y, Z);
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f || Camera.localPosition.x == Positions[currentPos])
            {
                t = 0.0f;
                isMoving = false;
            }
        }

        if (currentPos == 0)
            ControlsRight.SetActive(false);
        else
            ControlsRight.SetActive(true);
    }

    public void Next()
    {
        if (!isMoving)
        {
            if (currentPos + 1 < Positions.Count)
            {
                isMoving = true;
                currentPos += 1;

                if (Texts != null)
                    Text.text = Texts[currentPos];
            }
            else if (GameObject.Find("SceneLoader"))
            {
                GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();
            }
        }
               
    }

    public void Previous()
    {
        if (currentPos > 0 && !isMoving)
        {
            isMoving = true;
            currentPos -= 1;

            if (Texts != null)
                Text.text = Texts[currentPos];
        }      
    }

    public void Skip()
    {
        if (currentPos < Positions.Count && !isMoving)
        {
            isMoving = true;
            currentPos = Positions.Count - 1;

            if (Texts != null)
                Text.text = Texts[currentPos];
        }
    }

    public void GetTexts(string fileName)
    {
#if UNITY_WEBGL
        StartCoroutine(GetFile(fileName));
#else
        StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/" + fileName + ".json");
        json = reader.ReadToEnd();
        Texts = JsonUtility.FromJson<StoryText>(json).Texts;
        Text.text = Texts[currentPos];
#endif
    }

    private IEnumerator GetFile(string fileName)
    {
        gettingTexts = true;
        using (UnityWebRequest req = UnityWebRequest.Get("https://raw.githubusercontent.com/keckluis/47Ronin/main/Assets/StreamingAssets/" + fileName + ".json"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ProtocolError || req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(req.error);
            }
            else
            {
                json = req.downloadHandler.text;
                Texts = JsonUtility.FromJson<StoryText>(json).Texts;
                Text.text = Texts[currentPos];
            }
        }
        gettingTexts = false;
    }
}

public class StoryText
{
    public List<string> Texts;
}