using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions_Outro : MonoBehaviour
{
    private bool isWalking = false;
    private Animator Animator;
    public Controls ActionMap;
    public float Speed = 1;
    public SceneChanger SceneChanger;
    public bool facingLeft = true;

    void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Outro.Ride.performed += Ride;
        ActionMap.Outro.Ride.canceled += Stop;
    }

    private void OnDestroy()
    {
        ActionMap.Outro.Ride.performed -= Ride;
        ActionMap.Outro.Ride.canceled -= Stop;
        ActionMap.Disable();
    }

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            if (facingLeft)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            transform.Translate(-Speed, 0, 0);
        }
    }

    private void Update()
    {
        if (SceneChanger.NextScene)
        {
            SceneChanger.NextScene = false;
            ActionMap.Outro.Ride.performed -= Ride;
            ActionMap.Outro.Ride.canceled -= Stop;
            ActionMap.Disable();
            if (SceneChanger.SceneLoader != null)
                SceneChanger.SceneLoader.LoadNextScene();
        }      
    }

    private void Ride(InputAction.CallbackContext context)
    {
        float direction = context.ReadValue<float>();

        if (direction < 0)
            facingLeft = true;
        else if (direction > 0)
            facingLeft = false;

        isWalking = true;
    }

    private void Stop(InputAction.CallbackContext context)
    {
        isWalking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("END");

        StartCoroutine(WaitForEnd());
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(3);
        SceneChanger.NextScene = true;
    }
}
