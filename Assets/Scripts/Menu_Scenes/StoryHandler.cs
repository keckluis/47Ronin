using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class StoryHandler : MonoBehaviour
{
    public Transform Camera;
    public float Y = 0;
    public float Z = 0;

    public List<float> Positions;

    public string TextFileName;
    public TextMeshProUGUI Text;

    [SerializeField] private int currentPos = 0;
    private bool isMoving = false;
    static float t = 0.0f;

    private StoryLevel TextsSource;
    private List<string> Texts = new List<string>();

    public GameObject ControlsRight;

    Language lang;

    public Controls ActionMap;

    private void Awake()
    {
        currentPos = 0;
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Story.Next.started += Next;
        ActionMap.Story.Previous.started += Previous;
    }

    private void Start()
    {
        if (GameObject.Find("Language"))
        {
            lang = GameObject.Find("Language").GetComponent<Language>();
            
            foreach(StoryLevel slvl in StoryLevels.Lvls)
            {
                if (slvl.Name == TextFileName)
                {                    
                    TextsSource = slvl;
                    if (lang.currentLanguage == Languages.German)
                    {
                        Texts = TextsSource.TextsDE;
                    }
                    else if (lang.currentLanguage == Languages.English)
                    {
                        Texts = TextsSource.TextsEN;
                    }
                    break;
                }
            }
        }
    }

    private void Update()
    {
        if (lang.currentLanguage == Languages.German)
        {
            Texts = TextsSource.TextsDE;
        }
        else if (lang.currentLanguage == Languages.English)
        {
            Texts = TextsSource.TextsEN;
        }

        Text.text = Texts[currentPos];
    }

    private void FixedUpdate()
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

        if (currentPos == 0)
            ControlsRight.SetActive(false);
        else
            ControlsRight.SetActive(true);
    }

    public void Next(InputAction.CallbackContext ctx)
    {
        if (!isMoving)
        {
            if (currentPos + 1 < Positions.Count)
            {
                isMoving = true;
                currentPos += 1;

                if (Texts != null)
                    Text.text = Texts[currentPos];
            }
            else if (GameObject.Find("SceneLoader"))
            {
                GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();
                ActionMap.Story.Next.started -= Next;
                ActionMap.Story.Previous.started -= Previous;
                ActionMap.Disable();
            }
        }
    }

    public void NextButton()
    {
        if (!isMoving)
        {
            if (currentPos + 1 < Positions.Count)
            {
                isMoving = true;
                currentPos += 1;

                if (Texts != null)
                    Text.text = Texts[currentPos];
            }
            else if (GameObject.Find("SceneLoader"))
            {
                GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();
                ActionMap.Story.Next.started -= Next;
                ActionMap.Story.Previous.started -= Previous;
                ActionMap.Disable();
            }
        }
    }

    public void Previous(InputAction.CallbackContext ctx)
    {
        if (currentPos > 0 && !isMoving)
        {
            isMoving = true;
            currentPos -= 1;

            if (Texts != null)
                Text.text = Texts[currentPos];
        }
    }

    public void PreviousButton()
    {
        if (currentPos > 0 && !isMoving)
        {
            isMoving = true;
            currentPos -= 1;

            if (Texts != null)
                Text.text = Texts[currentPos];
        }
    }

    public void Skip(InputAction.CallbackContext ctx)
    {
        if (currentPos < Positions.Count && !isMoving)
        {
            isMoving = true;
            currentPos = Positions.Count - 1;

            if (Texts != null)
                Text.text = Texts[currentPos];
        }
    }
}