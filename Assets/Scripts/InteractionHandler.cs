using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    public List<InteractableObject> Objects;
    private List<string> Texts;

    public AudioManager AudioManager;

    public bool Cooldown = false;

    private string json;
    private bool gettingTexts = false;

    private Languages lang = Languages.German;

    private void Start()
    {
        GetTexts("11_FindHideout");

        if (GameObject.Find("Language"))
        {
            lang = GameObject.Find("Language").GetComponent<Language>().currentLanguage;
        }
    }

    private void Update()
    {
        if (Texts == null && !gettingTexts)
        {
            GetTexts("11_FindHideout");
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

    public void GetTexts(string fileName)
    {
#if UNITY_WEBGL
        StartCoroutine(GetFile(fileName));
#else
        StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/" + fileName + ".json");
        json = reader.ReadToEnd();
        if (lang == Languages.German)
            Texts = JsonUtility.FromJson<StoryText>(json).TextsDE;
        else if (lang == Languages.English)
            Texts = JsonUtility.FromJson<StoryText>(json).TextsEN;
#endif
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(2);
        Cooldown = false;
    }

    private IEnumerator GetFile(string fileName)
    {
        gettingTexts = true;
        using (UnityWebRequest req = UnityWebRequest.Get("https://raw.githubusercontent.com/keckluis/47Ronin/main/Assets/StreamingAssets/" + fileName + ".json"))
        {
            yield return req.SendWebRequest();
            if (req.result == UnityWebRequest.Result.ProtocolError || req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(req.error);
            }
            else
            {
                json = req.downloadHandler.text;
                if (lang == Languages.German)
                    Texts = JsonUtility.FromJson<StoryText>(json).TextsDE;
                else if (lang == Languages.English)
                    Texts = JsonUtility.FromJson<StoryText>(json).TextsEN;
            }
        }
        gettingTexts = false;
    }
}
