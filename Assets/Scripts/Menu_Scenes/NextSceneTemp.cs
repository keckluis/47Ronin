using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneTemp : MonoBehaviour
{
   public void NextScene()
   {
        if (GameObject.Find("SceneLoader"))
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();
   }
}
