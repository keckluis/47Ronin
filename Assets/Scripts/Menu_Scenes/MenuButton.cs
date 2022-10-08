using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private UIButtons uiButtons;
    void Start()
    {
        if (GameObject.Find("MenuCanvas"))
        {
            uiButtons = GameObject.Find("MenuCanvas").GetComponent<UIButtons>();

            gameObject.GetComponent<Button>().onClick.AddListener(uiButtons.OpenMenu);
        }
    }
}
