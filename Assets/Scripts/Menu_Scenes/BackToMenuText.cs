using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackToMenuText : MonoBehaviour
{
    Language Language;
    TextMeshProUGUI Text;
    public TextMeshProUGUI Yes;
    public TextMeshProUGUI No;

    void Start()
    {
        Language = GameObject.Find("Language").GetComponent<Language>();
        Text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Language.currentLanguage == Languages.English)
        {
            Text.text = "Back to main menu?";
            Yes.text = "YES";
            No.text = "NO";
        }
        else
        {
            Text.text = "Zurück ins Hauptmenü?";
            Yes.text = "JA";
            No.text = "NEIN";
        }

    }
}
