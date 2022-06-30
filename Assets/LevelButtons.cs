using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LevelButtons : MonoBehaviour
{
    private SceneLoader sceneLoader;
    public MainMenuButtons MMButtons;

    public List<Image> Levels;
    public List<int> LevelIndexes;
    public Controls ActionMap;
    private bool activated = false;

    private int activeLevel = 0;

    private bool moved = false;

    void Start()
    {
        if (GameObject.Find("SceneLoader"))
        {
            sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        }
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && !activated)
        {
            activated = true;
            ActionMap = new Controls();

            ActionMap.Enable();
            ActionMap.UI.LevelSelect.started += SelectLevel;
            ActionMap.UI.LevelSelect.canceled += MoveDone;
            ActionMap.UI.A.performed += LoadScene;
        }
        else if (!gameObject.activeInHierarchy && activated)
        {
            ActionMap.UI.LevelSelect.started -= SelectLevel;
            ActionMap.UI.LevelSelect.canceled -= MoveDone;
            ActionMap.UI.A.performed -= LoadScene;
            ActionMap.Disable();
            activated = false;
        }
    }

    private void OnDestroy()
    {     
        ActionMap.UI.LevelSelect.started -= SelectLevel;
        ActionMap.UI.LevelSelect.canceled -= MoveDone;
        ActionMap.UI.A.performed -= LoadScene;
        ActionMap.Disable();
    }

    private void SelectLevel(InputAction.CallbackContext context)
    {
        if (!moved)
        {
            moved = true;
            float input = context.ReadValue<float>();

            if (input < 0)
            {
                if (activeLevel == 0)
                    OnPointerEnter(Levels.Count - 1);
                else
                    OnPointerEnter(activeLevel - 1);
            }
            else if (input > 0)
            {
                if (activeLevel == Levels.Count - 1)
                    OnPointerEnter(0);
                else
                    OnPointerEnter(activeLevel + 1);
            }
        } 
    }

    private void MoveDone(InputAction.CallbackContext context)
    {
        moved = false;
    }

    private void LoadScene(InputAction.CallbackContext context)
    {
        LoadScence(LevelIndexes[activeLevel]);
    }
    public void LoadScence(int index)
    {
        if (sceneLoader != null)
        {
            MMButtons.RemoveControls();
            sceneLoader.LoadSpecificScene(index);
        }
    }

    public void OnPointerEnter(int index)
    {
        Levels[index].color = Color.white;
        activeLevel = index;

        foreach(Image lvl in Levels)
        {
            if (lvl != Levels[index])
                lvl.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void OnPointerExit(int index)
    {
        Levels[index].color = new Color(1, 1, 1, 0.5f);
    }
}
