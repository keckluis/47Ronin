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
            }
        }
        else
        {
            RoomDivider = GetComponent<InteractableObject>();
        }
    }
}
