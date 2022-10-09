using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Controls ActionMap;
    public Transform ThrowPos;

    public GameObject stone;

    private Animator Animator;

    private bool isWalking = false;
    private bool actionPlaying = false;
    private float speed;
    public Facing direction = Facing.LEFT;
    public float Speed = 0.03f;

    bool isAiming = false;
    public float LaunchForce = 750;

    private float x = 0;
    private float y = 0;
    private Vector2 shotDirection;
    private float keyboardAim = 0;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    public SceneChanger SceneChanger;
    bool isThrowing = false;
    float keyboardRadians;
    bool isAimingKeyboard = false;

    void Awake()
    {
        ActionMap = new Controls();

        if (GetComponent<Animator>() != null)
            Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ActionMap.Level05.Enable();
        
        ActionMap.Level05.Walk.started += Walk;
        ActionMap.Level05.Walk.canceled += StopWalking;

        ActionMap.Level05.Throw.performed += Throw;
        ActionMap.Level05.Aim.performed += Aim;
        ActionMap.Level05.Aim.canceled += StopAim;
        ActionMap.Level05.AimKeyboard.started += AimKeyboard;
        ActionMap.Level05.AimKeyboard.canceled += StopAimKeyboard;

    }

    private void OnDestroy()
    {
        ActionMap.Level05.Disable();

        ActionMap.Level05.Walk.started -= Walk;
        ActionMap.Level05.Walk.canceled -= StopWalking;

        ActionMap.Level05.Throw.performed -= Throw;
        ActionMap.Level05.Aim.performed -= Aim;
        ActionMap.Level05.Aim.canceled -= StopAim;
        ActionMap.Level05.AimKeyboard.started -= AimKeyboard;
        ActionMap.Level05.AimKeyboard.canceled -= StopAimKeyboard;
    }

    private void Start()
    {
        points = new GameObject[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, ThrowPos.position, Quaternion.identity);
        }
    }

    public void AimKeyboard(InputAction.CallbackContext context)
    {
        keyboardAim = context.ReadValue<float>();
    }

    public void StopAimKeyboard(InputAction.CallbackContext context)
    {
        keyboardAim = 0;
        //isAimingKeyboard = false;
    }

    public void Aim(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        x = input.x;
        y = input.y;

        if (x == 0 && y == 0)
        {
            isAiming = false;
        }
        else
        {
            isAiming = true;
        }   
    }

    public void StopAim(InputAction.CallbackContext context)
    {
        isAiming = false;
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if ((isAiming || isAimingKeyboard) && !actionPlaying && !isThrowing)
        {
            Animator.SetTrigger("throw");
            actionPlaying = true;
            isThrowing = true;
            isAimingKeyboard = false;
            keyboardRadians = 0;
        }
    }

    public void StoneFlight()
    {
        GameObject arrow = Instantiate(stone, ThrowPos.position, ThrowPos.rotation);

        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * LaunchForce;
    }

    public void ThrowDone()
    {
        isThrowing = false;
        actionPlaying = false;
    }

    public void Walk(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (input != null && !actionPlaying)
        {
            if (input.x < 0)
            {
                direction = Facing.LEFT;
                speed = Speed;
                isWalking = true;
            }
            else if (input.x > 0)
            {
                direction = Facing.RIGHT;
                speed = Speed;
                isWalking = true;
            }
            else
                speed = 0;

            float dir;
            if (direction == Facing.LEFT)
                dir = 0;
            else
                dir = 180;
            transform.localRotation = Quaternion.Euler(new Vector3(0, dir, 0));

            if (input.x == 0 && input.y == 0)
                isWalking = false;
            return;
        }
        isWalking = false;
    }

    public void StopWalking(InputAction.CallbackContext ctx)
    {
        isWalking = false;
    }

    void FixedUpdate()
    {
        Animator.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            transform.Translate(-speed, 0, 0);
        }

        if (keyboardAim != 0)
        {
            isAimingKeyboard = true;
            keyboardRadians += keyboardAim * 0.05f;

            Vector2 keyboardVector = new Vector2(Mathf.Sin(keyboardRadians), Mathf.Cos(keyboardRadians));
            x = keyboardVector.x;
            y = keyboardVector.y;
        }

        if (isWalking)
        {
            isAimingKeyboard = false;
            keyboardRadians = 0;
        }

        shotDirection = new Vector2(x, y);
    }

    private void Update()
    {
        if (SceneChanger.NextScene || SceneChanger.GameOver)
        {
            ActionMap.Level05.Walk.started -= Walk;
            ActionMap.Level05.Walk.canceled -= StopWalking;

            ActionMap.Level05.Throw.performed -= Throw;
            ActionMap.Level05.Aim.performed -= Aim;
            ActionMap.Level05.Aim.canceled -= StopAim;
            ActionMap.Level05.AimKeyboard.started -= AimKeyboard;
            ActionMap.Level05.AimKeyboard.canceled -= StopAimKeyboard;
            ActionMap.Level05.Disable();

            if (SceneChanger.GameOver)
            {
                if (SceneChanger.SceneLoader != null)
                    SceneChanger.SceneLoader.LoadGameOver();
            }
            else if (SceneChanger.NextScene)
            {
                if (SceneChanger.SceneLoader != null)
                    SceneChanger.SceneLoader.LoadNextScene();
            }

            Destroy(this);
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            if ((isAiming || isAimingKeyboard) && !isThrowing)
                points[i].SetActive(true);
            else
                points[i].SetActive(false);

            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        } 
    }

    Vector3 PointPosition(float t)
    {
        Vector2 position = (Vector2)ThrowPos.position + (shotDirection.normalized * LaunchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return new Vector3(position.x, position.y, -2);
    }
}
