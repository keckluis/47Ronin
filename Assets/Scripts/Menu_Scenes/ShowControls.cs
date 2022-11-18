using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowControls : MonoBehaviour
{
    public Image LS_1D;
    public Image LS_2D;
    public Image RS;
    public Image LT;
    public Image RT;

    public string LSFunction;
    public string RSFunction;
    public string LTFunction;
    public string RTFunction;
    public int WalkDimensions;

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
            WalkDimensions = 0;
        }

        int count = 0;
        if (LSFunction != "")
        {
            if (WalkDimensions == 1)
            {
                LS_1D.transform.localPosition = new Vector3(-2, 770 - (count * 70), 0);
                LS_1D.color = Color.white;
                LS_1D.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LSFunction;
                LS_2D.color = new Color(1, 1, 1, 0);
                LS_2D.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                LS_2D.transform.localPosition = new Vector3(-2, 770 - (count * 70), 0);
                LS_2D.color = Color.white;
                LS_2D.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LSFunction;
                LS_1D.color = new Color(1, 1, 1, 0);
                LS_1D.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            }

            count++;
        }
        else
        {
            LS_1D.color = new Color(1, 1, 1, 0);
            LS_1D.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            LS_2D.color = new Color(1, 1, 1, 0);
            LS_2D.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
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
