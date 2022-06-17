using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public List<InteractableObject> Objects;
    private List<string> Texts;

    private void Start()
    {
        Texts = StoryText.GetTexts("11_FindHideout").Texts;
    }

    public void Interact()
    {
        int i = 0;
        foreach(InteractableObject obj in Objects)
        {
            if (obj.OutlineVisible)
            {
                obj.Interaction(Texts[i]);
                return;
            }
            i++;
        }
    }
}
