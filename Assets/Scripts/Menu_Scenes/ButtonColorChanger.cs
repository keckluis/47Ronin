using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ButtonColorChanger : MonoBehaviour
{
    public Color Default = new Color(0, 0, 0);
    public Color OnHover = new Color(1, 1, 1);

    private TextMeshProUGUI ButtonLabel;
    private Image Image;

    private void OnEnable()
    {
        ToDefaultColor();
    }

    private void Start()
    {
        if (transform.GetChild(0).GetComponent<TextMeshProUGUI>())
            ButtonLabel = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        else if (transform.GetChild(0).GetComponent<Image>())
            Image = transform.GetChild(0).GetComponent<Image>();

    }
    private void OnDestroy()
    {
        ToDefaultColor();
    }

    public void OnPointerEnter()
    {
        if (ButtonLabel != null)
            ButtonLabel.color = OnHover;
        else if (Image != null)
            Image.color = OnHover;
    }

    public void OnPointerExit()
    {
        ToDefaultColor();
    }

    public void OnClick()
    {
        ToDefaultColor();
    }
    void ToDefaultColor()
    {
        if (ButtonLabel != null)
            ButtonLabel.color = Default;
        else if (Image != null)
            Image.color = Default;
    }
}
