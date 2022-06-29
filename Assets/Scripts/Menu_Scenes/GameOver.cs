using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOver : MonoBehaviour
{
    public Button RestartButton;
    public Button BackToMenuBtn;
    void Start()
    {
        if (GameObject.Find("MenuCanvas"))
        {
            RestartButton.onClick.AddListener(GameObject.Find("MenuCanvas").GetComponent<UIButtons>().RestartLevel);
            BackToMenuBtn.onClick.AddListener(GameObject.Find("MenuCanvas").GetComponent<UIButtons>().BackToMenu);
            GameObject.Find("MenuCanvas").GetComponent<UIButtons>().RestartButtonText = RestartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }
}
