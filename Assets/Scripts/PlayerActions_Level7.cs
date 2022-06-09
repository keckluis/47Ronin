using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions_Level7 : MonoBehaviour
{
    private Animator Animator;
    private bool isWalking = false;
    private bool isStriking = false;
    private bool walkState;
    public float Speed;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Animator.SetBool("isWalking", isWalking);
        if (isWalking)
            transform.Translate(-Speed, 0, 0);
    }

    public void Walk(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input != null && !isStriking)
        {
            if (input.x < 0)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                isWalking = true;
            }
            else if (input.x > 0)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                isWalking = true;
            }
            else
                isWalking = false;
        }
        else
        {
            isWalking = false;
        }
    }

    public void Strike()
    {
        if (!isStriking)
        {
            isStriking = true;
            walkState = isWalking;
            isWalking = false;
            Animator.SetTrigger("strike");
        }   
    }

    public void StrikeFinished()
    {
       isStriking = false;
       isWalking = walkState;
    }
}
