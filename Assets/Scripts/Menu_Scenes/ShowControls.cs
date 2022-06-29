using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowControls : MonoBehaviour
{
    public Image LS;
    public Image RS;
    public Image LT;
    public Image RT;

    public string LSFunction;
    public string RSFunction;
    public string LTFunction;
    public string RTFunction;

    void Update()
    {
        if (LSFunction != "")
        {
            LS.color = Color.white;
            LS.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LSFunction;
        }
        else
        {
            LS.color = new Color(1, 1, 1, 0);
            LS.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        if (RSFunction != "")
        {
            RS.color = Color.white;
            RS.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RSFunction;
        }
        else
        {
            RS.color = new Color(1, 1, 1, 0);
            RS.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        if (LTFunction != "")
        {
            LT.color = Color.white;
            LT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LTFunction;
        }
        else
        {
            LT.color = new Color(1, 1, 1, 0);
            LT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        if (RTFunction != "")
        {
            RT.color = Color.white;
            RT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RTFunction;
        }
        else
        {
            RT.color = new Color(1, 1, 1, 0);
            RT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
