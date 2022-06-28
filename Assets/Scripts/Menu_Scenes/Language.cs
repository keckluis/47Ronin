using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
    public List<string> FileNamesUI;
    public ShowControls Controls;

    private bool getttingControls = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!gettingTexts && !getttingControls)
        {
            if (StoryLevels.Lvls.Count < FileNames.Count)
            {
                GetTexts(FileNames[StoryLevels.Lvls.Count]);
            }
            else
                getttingControls = true;
        }
        if (!gettingTexts && getttingControls)
        {
            if (ControlSchemes.Schemes.Count < FileNamesUI.Count)
            {
                GetTexts(FileNamesUI[ControlSchemes.Schemes.Count]);
            }
        }

        string sceneName = SceneManager.GetActiveScene().name;
        string file = "";
        if (sceneName.Contains("Text") || sceneName == "02_Intro")
        {
            file = "UI_Story";
        }
        else if (sceneName == "03_HouseScouting")
        {
            file = "UI_HouseScouting";
        }
        else if (sceneName == "05d_SneakingThroughGarden" || sceneName == "05e_SneakingThroughGarden")
        {
            file = "UI_SneakingThroughGarden";
        }
        else if (sceneName == "07_Battle")
        {
            file = "UI_Battle";
        }
        else if (sceneName == "09_AlarmBell")
        {
            file = "UI_AlarmBell";
        }
        else if (sceneName == "11_FindHideout")
        {
            file = "UI_FindHideout";
        }
        else if (sceneName == "13_BossFight")
        {
            file = "UI_BossFight";
        }
        else if (sceneName == "15_Outro")
        {
            file = "UI_Outro";
        }

        foreach (ControlScheme cs in ControlSchemes.Schemes)
        {
            if (cs.Name == file)
            {
                if (currentLanguage == Languages.German)
                {
                    Controls.LSFunction = cs.LS_DE;
                    Controls.RSFunction = cs.RS_DE;
                    Controls.LTFunction = cs.LT_DE;
                    Controls.RTFunction = cs.RT_DE;
                }
                else if (currentLanguage == Languages.English)
                {
                    Controls.LSFunction = cs.LS_EN;
                    Controls.RSFunction = cs.RS_EN;
                    Controls.LTFunction = cs.LT_EN;
                    Controls.RTFunction = cs.RT_EN;
                }
                break;
            }
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

        if (!getttingControls)
            StoryLevels.Lvls.Add(JsonUtility.FromJson<StoryLevel>(json));
        else
            ControlSchemes.Schemes.Add(JsonUtility.FromJson<ControlScheme>(json));
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
                Debug.Log(fileName + ": " + req.error);
            }
            else
            {
                string json = req.downloadHandler.text;

                if (!getttingControls)
                    StoryLevels.Lvls.Add(JsonUtility.FromJson<StoryLevel>(json));
                else
                    ControlSchemes.Schemes.Add(JsonUtility.FromJson<ControlScheme>(json));
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

public static class ControlSchemes
{
    public static List<ControlScheme> Schemes = new List<ControlScheme>();
}

public class ControlScheme
{
    public string Name;
    public string LS_DE;
    public string RS_DE;
    public string LT_DE;
    public string RT_DE;
    public string LS_EN;
    public string RS_EN;
    public string LT_EN;
    public string RT_EN;
}