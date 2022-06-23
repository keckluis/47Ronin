using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ButtonColorChanger : MonoBehaviour
{
    public Color Default = new Color(0, 0, 0);
    public Color OnHover = new Color(1, 1, 1);

    private TextMeshProUGUI ButtonLabel;

    private void OnEnable()
    {
        if (ButtonLabel != null)
            ButtonLabel.color = Default;
    }
    private void Start()
    {
        ButtonLabel = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter()
    {
        ButtonLabel.color = OnHover;
    }

    public void OnPointerExit()
    {
        ButtonLabel.color = Default;
    }

    public void OnClick()
    {
        ButtonLabel.color = Default;
    }
}
