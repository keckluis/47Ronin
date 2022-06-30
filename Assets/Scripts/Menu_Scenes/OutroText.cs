using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutroText : MonoBehaviour
{
    public List<TextMeshProUGUI> OutroTexts;
    public string TextFileName;
    private List<string> Texts;
    private StoryLevel TextsSource;
    Language lang;


    void Start()
    {
        if (GameObject.Find("Language"))
        {
            lang = GameObject.Find("Language").GetComponent<Language>();

            foreach (StoryLevel slvl in StoryLevels.Lvls)
            {
                if (slvl.Name == TextFileName)
                {
                    TextsSource = slvl;
                    if (lang.currentLanguage == Languages.German)
                    {
                        Texts = TextsSource.TextsDE;
                    }
                    else if (lang.currentLanguage == Languages.English)
                    {
                        Texts = TextsSource.TextsEN;
                    }
                    break;
                }
            }

            int i = 0;
            foreach(string txt in Texts)
            {
                OutroTexts[i].text = txt;
                i++;
            }
        }
    }   
}
