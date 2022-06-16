using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class StoryHandler : MonoBehaviour
{
    public Transform Camera;
    public float Y = 0;
    public float Z = 0;

    public List<float> Positions;

    public string TextFileName;
    public TextMeshProUGUI Text;

    private int currentPos = 0;
    private bool isMoving = false;
    static float t = 0.0f;

    private List<string> Texts;

    private void Start()
    {
        Texts = StoryText.GetTexts(TextFileName).Texts;
        Text.text = Texts[currentPos];
    }
    private void Update()
    {
        if (isMoving)
        {
            Camera.localPosition = new Vector3(Mathf.Lerp(Camera.localPosition.x, Positions[currentPos], t), Y, Z);
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f || Camera.localPosition.x == Positions[currentPos])
            {
                t = 0.0f;
                isMoving = false;
            }
        }
    }

    public void Next()
    {
        if (currentPos + 1 < Positions.Count && !isMoving)
        {
            isMoving = true;
            currentPos += 1;

            Text.text = Texts[currentPos];
        }    
    }

    public void Previous()
    {
        if (currentPos > 0 && !isMoving)
        {
            isMoving = true;
            currentPos -= 1;

            Text.text = Texts[currentPos];
        }      
    }
}

class StoryText
{
    public List<string> Texts;
    public static StoryText GetTexts(string fileName)
    {
        StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/" + fileName + ".json");
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<StoryText>(json);
    }
}