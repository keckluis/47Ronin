using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservingGuard : MonoBehaviour
{
    int detectionCounter;
    bool detected;
    public GameObject detectionStatusIcon; 

    public void FixedUpdate()
    {
            if (detected && detectionCounter < 200)
            {
                detectionCounter++;
            }
            else if (!detected && detectionCounter > 0)
                detectionCounter--;
       // }
        Debug.Log(detectionCounter);
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
}
