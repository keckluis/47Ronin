using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintHandler : MonoBehaviour
{
    public string TextFileName;
    public TextMeshProUGUI Text;
    private StoryLevel TextsSource;
    private List<string> Texts = new List<string>();
    Language lang;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Language"))
        {
            lang = GameObject.Find("Language").GetComponent<Language>();
            
            foreach(StoryLevel slvl in StoryLevels.Lvls)
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
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (lang.currentLanguage == Languages.German)
        {
            Texts = TextsSource.TextsDE;
        }
        else if (lang.currentLanguage == Languages.English)
        {
            Texts = TextsSource.TextsEN;
        }

        Text.text = Texts[Texts.Count -1];
    }
}
