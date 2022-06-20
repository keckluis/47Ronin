using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions_Level11 : MonoBehaviour
{
    public Facing direction = Facing.LEFT;
    private Animator Animator;
    private bool isWalking = false;

    public float Speed;
    private float speedHori;
    private float speedVert;

    public GameObject Text;

    public InteractionHandler InteractionHandler;

    public Controls ActionMap;

    private void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Level11.Walk.performed += Walk;
        ActionMap.Level11.Walk.canceled += StopWalking;
        ActionMap.Level11.Interact.performed += InteractionHandler.Interact;
    }

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            transform.Translate(-speedHori, speedVert, 0);
        }
    }

    public void Walk(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input != null)
        {

            if (input.x == 0 && input.y == 0)
            {
                speedHori = 0;
                speedVert = 0;
                isWalking = false;
                return;
            }

            isWalking = true;

            if (input.x < 0)
            {
                direction = Facing.LEFT;
            }
            else if (input.x > 0)
            {
                direction = Facing.RIGHT;
            }

            speedHori = Mathf.Abs(input.x * Speed);
            speedVert = input.y * Speed;

            float dir;
            if (direction == Facing.LEFT)
                dir = 0;
            else
                dir = 180;
            transform.localRotation = Quaternion.Euler(new Vector3(0, dir, 0));
            Text.transform.localRotation = Quaternion.Euler(new Vector3(0, dir, 0));
        }
    }

    public void StopWalking(InputAction.CallbackContext context)
    {
        isWalking = false;
    }
}
