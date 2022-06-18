using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public List<InteractableObject> Objects;
    private List<string> Texts;

    public AudioManager AudioManager;

    public bool Cooldown = false;
    private void Start()
    {
        Texts = StoryText.GetTexts("11_FindHideout").Texts;
    }

    public void Interact()
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
