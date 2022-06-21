using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamCreditsLang : MonoBehaviour
{
    public GameObject TeamDE;
    public GameObject TeamEN;

    private Language lang;

    void Start()
    {
       if (GameObject.Find("Language"))
       {
            lang = GameObject.Find("Language").GetComponent<Language>();
       } 
    }
    void Update()
    {
        if (lang != null)
        {
            if (lang.currentLanguage == Languages.German)
            {
                TeamDE.SetActive(true);
                TeamEN.SetActive(false);
            }
            else if (lang.currentLanguage == Languages.English)
            {
                TeamDE.SetActive(false);
                TeamEN.SetActive(true);
            }
        }
    }
}
