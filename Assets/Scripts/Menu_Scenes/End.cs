using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public Button BackToMenuBtn;
    void Start()
    {
        if (GameObject.Find("MenuCanvas"))
        {
            BackToMenuBtn.onClick.AddListener(GameObject.Find("MenuCanvas").GetComponent<UIButtons>().BackToMenu); 
        }
    }

}
