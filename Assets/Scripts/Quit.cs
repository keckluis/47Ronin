using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quit : MonoBehaviour
{
    private Language Language;
    string German = "VERLASSEN";
    string English = "QUIT";

    void Start()
    {
    #if UNITY_WEBGL
        gameObject.SetActive(false);
    #endif

        if (GameObject.Find("Language"))
            Language = GameObject.Find("Language").GetComponent<Language>();
    }

    void Update()
    {
        if (Language != null)
        {
            if (Language.currentLanguage == Languages.German)
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = German;
            else if (Language.currentLanguage == Languages.English)
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = English;
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
