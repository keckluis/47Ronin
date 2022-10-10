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

    private SceneLoader SceneLoader;

    private void Start()
    {
        if (GameObject.Find("SceneLoader"))
            SceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
    }

    void Update()
    {
        if (SceneLoader.Loading)
        {
            LSFunction = "";
            RSFunction = "";
            LTFunction = "";
            RTFunction = "";
        }

        int count = 0;
        if (LSFunction != "")
        {
            LS.transform.localPosition = new Vector3(-2, 770 - (count * 70), 0);
            LS.color = Color.white;
            LS.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LSFunction;
            count++;
        }
        else
        {
            LS.color = new Color(1, 1, 1, 0);
            LS.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        if (RSFunction != "")
        {
            RS.transform.localPosition = new Vector3(-2, 770 - (count * 70), 0);
            RS.color = Color.white;
            RS.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RSFunction;
            count++;
        }
        else
        {
            RS.color = new Color(1, 1, 1, 0);
            RS.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        if (LTFunction != "")
        {
            LT.transform.localPosition = new Vector3(-2, 770 - (count * 70), 0);
            LT.color = Color.white;
            LT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LTFunction;
            count++;
        }
        else
        {
            LT.color = new Color(1, 1, 1, 0);
            LT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        if (RTFunction != "")
        {
            RT.transform.localPosition = new Vector3(-2, 770 - (count * 70), 0);
            RT.color = Color.white;
            RT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RTFunction;
            count++;
        }
        else
        {
            RT.color = new Color(1, 1, 1, 0);
            RT.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
