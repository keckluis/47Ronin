using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{    
    void Start()
    {
    #if UNITY_WEBGL
        gameObject.SetActive(false);
    #endif
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
