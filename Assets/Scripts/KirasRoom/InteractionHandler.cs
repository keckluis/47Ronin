using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public List<InteractableObject> Objects;
    private StoryLevel TextsSource;
    private List<string> Texts;
    public string TextFileName;

    public AudioManager AudioManager;

    public bool Cooldown = false;

    Language lang;

    private void Start()
    {
        if (GameObject.Find("Language"))
        {
            lang = GameObject.Find("Language").GetComponent<Language>();

            foreach (StoryLevel slvl in StoryLevels.Lvls)
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
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (!Cooldown)
        {
            int i = 0;
            foreach (InteractableObject obj in Objects)
            {
                if (obj.OutlineVisible)
                {
                    obj.Interaction(Texts[i]);
                    Cooldown = true;
                    StartCoroutine(StartCooldown());
                    AudioManager.PlayClip(0);
                    return;
                }
                i++;
            }
        }   
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(2);
        Cooldown = false;
    }
}
