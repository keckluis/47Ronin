using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public enum Languages
{
    German,
    English
}

public class Language : MonoBehaviour
{
    public Languages currentLanguage = Languages.German;
    private bool gettingTexts = false;

    public List<string> FileNames;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!gettingTexts && StoryLevels.Lvls.Count < FileNames.Count)
        {
            GetTexts(FileNames[StoryLevels.Lvls.Count]);
        }
    }

    public void SetLanguage(string lang)
    {
        print("Set language to " + lang);
        if (lang == "DE")
            currentLanguage = Languages.German;
        else if (lang == "EN")
            currentLanguage = Languages.English;
    }

    public void GetTexts(string fileName)
    {
#if UNITY_WEBGL
        StartCoroutine(GetFile(fileName));
#else
        StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/" + fileName + ".json");
        string json = reader.ReadToEnd();
        StoryLevels.Lvls.Add(JsonUtility.FromJson<StoryLevel>(json));
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
                string json = req.downloadHandler.text;
                StoryLevels.Lvls.Add(JsonUtility.FromJson<StoryLevel>(json));
            }
        }
        gettingTexts = false;
    }
}

public static class StoryLevels
{
    public static List<StoryLevel> Lvls = new List<StoryLevel>();
}

public class StoryLevel
{
    public string Name;
    public List<string> TextsDE;
    public List<string> TextsEN;

    public List<string> GetTexts(Languages lang)
    {
        if (lang == Languages.German)
            return TextsDE;
        else if (lang == Languages.English)
            return TextsEN;

        return null;
    }
}