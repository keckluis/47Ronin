using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.InputSystem;

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

    private Languages lang = Languages.German;

    public Controls ActionMap;

    private void Awake()
    {
        currentPos = 0;
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Story.Next.started += Next;
        ActionMap.Story.Previous.started += Previous;
        ActionMap.Story.Skip.started += Skip;
    }

    private void Start()
    {
        GetTexts(TextFileName);

        if (GameObject.Find("Language"))
        {
            lang = GameObject.Find("Language").GetComponent<Language>().currentLanguage;
        }
    }

    private void FixedUpdate()
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

    public void Next(InputAction.CallbackContext ctx)
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
                ActionMap.Story.Next.started -= Next;
                ActionMap.Story.Previous.started -= Previous;
                ActionMap.Story.Skip.started -= Skip;
                ActionMap.Disable();
            }
        }       
    }

    public void NextButton()
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
                ActionMap.Story.Next.started -= Next;
                ActionMap.Story.Previous.started -= Previous;
                ActionMap.Story.Skip.started -= Skip;
                ActionMap.Disable();
            }
        }
    }

    public void Previous(InputAction.CallbackContext ctx)
    {
        if (currentPos > 0 && !isMoving)
        {
            isMoving = true;
            currentPos -= 1;

            if (Texts != null)
                Text.text = Texts[currentPos];
        }      
    }

    public void PreviousButton()
    {
        if (currentPos > 0 && !isMoving)
        {
            isMoving = true;
            currentPos -= 1;

            if (Texts != null)
                Text.text = Texts[currentPos];
        }
    }

    public void Skip(InputAction.CallbackContext ctx)
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
        if (lang == Languages.German)
            Texts = JsonUtility.FromJson<StoryText>(json).TextsDE;
        else if (lang == Languages.English)
            Texts = JsonUtility.FromJson<StoryText>(json).TextsEN;
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
                if (lang == Languages.German)
                    Texts = JsonUtility.FromJson<StoryText>(json).TextsDE;
                else if (lang == Languages.English)
                    Texts = JsonUtility.FromJson<StoryText>(json).TextsEN;
                Text.text = Texts[currentPos];
            }
        }
        gettingTexts = false;
    }
}

public class StoryText
{
    public List<string> TextsDE;
    public List<string> TextsEN;
}