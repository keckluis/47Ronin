using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObservingGuard : MonoBehaviour
{
    int detectionCounter;
    bool detected;
    public GameObject detectionStatusIcon;
    private int AlarmTrigger = 80;
    public SceneChanger sceneChanger;

    private bool alarm = false;

    public void Start()
    {
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }


    public void FixedUpdate()
    {
        if (detected && detectionCounter < AlarmTrigger)
        {
            detectionCounter++;
        }
        else if (!detected && detectionCounter > 0)
            detectionCounter--;
        else if (detectionCounter == AlarmTrigger)
            AlarmTriggered();

       // }
    }
    public void raiseDetection()
    {
        detected = true;
        detectionStatusIcon.SetActive(detected);
    }
    public void lowerDetection()
    {
        detected = false;
        detectionStatusIcon.SetActive(detected);
    }
    public void AlarmTriggered()
    {
        if (!alarm)
        {
            print("GAME OVER");
            alarm = true;
            if (sceneChanger.SceneLoader != null)
                sceneChanger.GameOver = true;
        }
        //Scene scene = SceneManager.GetActiveScene(); 
        //SceneManager.LoadScene(scene.name);
    }
}
