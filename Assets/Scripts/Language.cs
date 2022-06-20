using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    public Languages currentLanguage = Languages.German;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetLanguage(string lang)
    {
        print("Set language to " + lang);
        if (lang == "DE")
            currentLanguage = Languages.German;
        else if (lang == "EN")
            currentLanguage = Languages.English;
    }
}

public enum Languages
{
    German,
    English
}
