using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiscoverRoomsHandler : MonoBehaviour
{
    public List<DiscoverRoom> Objects;
    private StoryLevel TextsSource;
    private List<string> Texts;
    public string TextFileName;

    public AudioManager AudioManager;
    public SceneChanger SceneChanger;

    public bool Cooldown = false;

    private int InteractedObjects = 0;


    Language lang;

    private void Start()
    {
        if (GameObject.Find("Language"))
        {
            lang = GameObject.Find("Language").GetComponent<Language>();

            Debug.Log("Storylevels " + StoryLevels.Lvls.Count);

            foreach (StoryLevel slvl in StoryLevels.Lvls)
            {
                if (slvl.Name == TextFileName)
                {
                    TextsSource = slvl;
                    if (lang.currentLanguage == Languages.German)
                    {
                        Texts = TextsSource.TextsDE;
                        Debug.Log("TextsDE Found!");
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

        if(InteractedObjects == Objects.Count) {
            Debug.Log("LEVEL END!!!!!");
            StartCoroutine(LevelEnd());
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!Cooldown)
        {
            int i = 0;
            foreach (DiscoverRoom obj in Objects)
            {
                if (obj.OutlineVisible)
                {
                    Debug.Log("interacted with: " + obj);
                    obj.Interaction(Texts[i]);
                    Cooldown = true;
                    StartCoroutine(StartCooldown(obj));
                    AudioManager.PlayClip(0);
                    
                    InteractedObjects++;
                    Debug.Log(InteractedObjects);
                    return;
                }
                i++;
            }
        }   
    }

    IEnumerator StartCooldown(DiscoverRoom obj)
    {
        yield return new WaitForSeconds(2);
        Cooldown = false;
        obj.GetComponent<DiscoverRoom>().enabled = false;
        obj.OutlineVisible = false;
    }

    IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(5);
        SceneChanger.NextScene = true;
    }
}
