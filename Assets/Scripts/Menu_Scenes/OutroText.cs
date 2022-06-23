using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class OutroText : MonoBehaviour
{
    public List<TextMeshProUGUI> OutroTexts;
    private List<string> Texts;
    private Languages lang = Languages.German;
    private string json;
    private bool gettingTexts = false;

    private void Start()
    {
        GetTexts("15_Outro");

        if (GameObject.Find("Language"))
        {
            lang = GameObject.Find("Language").GetComponent<Language>().currentLanguage;
        }
    }

    void Update()
    {
        if (Texts == null && !gettingTexts)
        {
            GetTexts("15_Outro");
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

        int i = 0;
        foreach(TextMeshProUGUI t in OutroTexts)
        {
            t.text = Texts[i];
            i++;
        }
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

                int i = 0;
                foreach (TextMeshProUGUI t in OutroTexts)
                {
                    t.text = Texts[i];
                    i++;
                }
            }
        }
        gettingTexts = false;
    }
}
