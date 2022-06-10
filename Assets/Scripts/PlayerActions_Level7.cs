using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Facing
{
    LEFT,
    RIGHT
}
public class PlayerActions_Level7 : MonoBehaviour
{
    private Animator Animator;
    private bool isWalking = false;
    private bool actionPlaying = false;
    private bool walkState;
   
    private float speedHori;
    private float speedVert;
    public Facing direction = Facing.LEFT;

    public float SpeedHorizontal = 0.03f;
    //public float SpeedVertical = 0.03f;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Animator.SetBool("isWalking", isWalking);
        if (isWalking)
            transform.Translate(-speedHori, 0, speedVert);
        if (transform.position.z > 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void Walk(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input != null && !actionPlaying)
        {
            if (input.x < 0)
            {
                direction = Facing.LEFT;
                speedHori = SpeedHorizontal;
                isWalking = true;
            }
            else if (input.x > 0)
            {
                direction = Facing.RIGHT;
                speedHori = SpeedHorizontal;
                isWalking = true;
            }
            else
                speedHori = 0;

            float dir;
            if (direction == Facing.LEFT)
                dir = 0;
            else
                dir = 180;
            transform.localRotation = Quaternion.Euler(new Vector3(0, dir, 0));

            /* if (input.y != 0)
            {
                if (direction == Facing.LEFT)
                    speedVert = +(input.y * SpeedVertical);
                else
                    speedVert = -(input.y * SpeedVertical);
                isWalking = true;
            }
            else
            {
                speedVert = 0;
            } */

            if (input.x == 0 && input.y == 0)
                isWalking = false;
            return;
        }
        isWalking = false;
    }

    public void Action(string action)
    {
        if (!actionPlaying)
        {
            actionPlaying = true;
            walkState = isWalking;
            isWalking = false;
            Animator.SetTrigger(action);
        }   
    }

    public void ActionFinished()
    {
       actionPlaying = false;
       isWalking = walkState;

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            isWalking = false;
        }
    }

    
}
