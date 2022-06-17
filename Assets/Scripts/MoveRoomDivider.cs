using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoomDivider : MonoBehaviour
{
    Animator Animator;
    InteractableObject RoomDivider;
    void Start()
    {
        Animator = GetComponent<Animator>();
        RoomDivider = GetComponent<InteractableObject>();
    }

    void Update()
    {
        if (RoomDivider != null)
        {
            if (RoomDivider.Interacted)
            {
                Animator.enabled = true;
                Debug.Log("SUCCESS");
                StartCoroutine(LevelEnd());
            }
        }
        else
        {
            RoomDivider = GetComponent<InteractableObject>();
        }
    }

    IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(5);
        if (GameObject.Find("SceneLoader"))
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();
        }
    }
}
