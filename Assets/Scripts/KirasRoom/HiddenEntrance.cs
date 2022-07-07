using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenEntrance : MonoBehaviour
{
    InteractableObject obj;
    bool success = false;
    public SceneChanger SceneChanger;

    private void Start()
    {
        obj = GetComponent<InteractableObject>();
    }

    private void Update()
    {
        if (obj != null)
        {
            if (obj.Interacted && !success)
            {
                success = true;
                Debug.Log("SUCCESS");
                StartCoroutine(LevelEnd());
            }
        }
        else
        {
            obj = GetComponent<InteractableObject>();
        }
    }

    IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(3);
        SceneChanger.NextScene = true;
    }
}
