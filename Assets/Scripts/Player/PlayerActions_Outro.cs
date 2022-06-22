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

    void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Outro.Ride.performed += Ride;
        ActionMap.Outro.Ride.canceled += Stop;
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
            transform.Translate(-Speed, 0, 0);
        }
    }

    private void Ride(InputAction.CallbackContext context)
    {
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
        if (GameObject.Find("SceneLoader"))
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();
        }
    }
}
