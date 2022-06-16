using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D PlayerRb;

    private Vector2 movmentDirection;
    private Vector2 ThrowDirection;
    public float maxSpeed;
    public Controls ActionMap;
    public Transform ThrowPos;

    public GameObject stone;

    private Animator Animator;
    
    void Awake()
    {
        ActionMap = new Controls();
        PlayerRb = GetComponent<Rigidbody2D>();

        if (GetComponent<Animator>() != null)
            Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ActionMap.PlayerControls.Enable();
        
        ActionMap.PlayerControls.ActivateThrowMode.started += activateThrowMode;
        ActionMap.PlayerControls.ActivateThrowMode.canceled += deactivateThrowMode;
        ActionMap.PlayerControls.StoneTrowing.performed += getThrowDirection;
        ActionMap.PlayerControls.StoneTrowing.canceled += StoneThrow;
        ActionMap.PlayerControls.Movement.performed += ctx => movmentDirection = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        ActionMap.PlayerControls.Disable();
    }

    void Movement(InputAction.CallbackContext ctx)
    {
        PlayerRb.velocity = ctx.ReadValue<Vector2>();
    }
    void activateThrowMode(InputAction.CallbackContext ctx)
    {
        ActionMap.PlayerControls.StoneTrowing.Enable();
        ActionMap.PlayerControls.Movement.Disable();
        movmentDirection = Vector2.zero;

    }
    void deactivateThrowMode(InputAction.CallbackContext ctx)
    {
        ActionMap.PlayerControls.StoneTrowing.Disable();
        ActionMap.PlayerControls.Movement.Enable();
    }
    void getThrowDirection(InputAction.CallbackContext ctx)
    {
        ThrowDirection = ctx.ReadValue<Vector2>();
        Debug.Log(ThrowDirection);
    }
    void StoneThrow(InputAction.CallbackContext ctx)
    {

        Animator.SetTrigger("throw");
    }

    private void throwStone()
    {
        GameObject stoneInst = Instantiate(stone, ThrowPos.position, ThrowPos.rotation);
        stoneInst.GetComponent<Rigidbody2D>().AddForce(ThrowDirection * 500);
    }

    void Update()
    {
        PlayerRb.AddForce(movmentDirection * 50);
        if (PlayerRb.velocity.magnitude > maxSpeed)
        {
            PlayerRb.velocity = Vector3.ClampMagnitude(PlayerRb.velocity, maxSpeed);
            //ActionMap.PlayerControls.Movement.performed += ctx => movmentDirection = ctx.ReadValue<Vector2>();
        }

        if (Animator != null)
        {
            if (movmentDirection.x == 0)
            {
                Animator.SetBool("isWalking", false);
            }
            else
            {
                Animator.SetBool("isWalking", true);

                if (movmentDirection.x < 0)
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                else
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
    }

}
