using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions_Level09 : MonoBehaviour
{
    public Transform Spine;
    private Animator Animator;
    public Animator Bow;
    private bool isShooting = false;
    private bool isAiming = false;
    public Transform Arrow;
    public GameObject ArrowPrefab;
    public AudioManager AudioManager;

    private float x = 0;
    private float y = 0;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        x = input.x;
        y = input.y;

        if (x > 0)
            return;

        if (x <= 0)
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        if (x == 0 && y == 0)
        {
            Spine.localRotation = Quaternion.Euler(0, 0, 0);
            isAiming = false; 
            return;
        }

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        
        Spine.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }  

    public void Shoot()
    {
        if (!isShooting && isAiming)
        {
            isShooting = true;
            Animator.SetTrigger("shoot");
            Bow.SetTrigger("shoot");
            AudioManager.PlayClip(0);
        }  
    }

    public void ArrowFlight()
    {
        Arrow.gameObject.SetActive(false);
        GameObject arrow = Instantiate(ArrowPrefab);
        arrow.transform.position = Arrow.position;

        arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(x * 750 - 1, y * 750));
    }

    public void ShotDone()
    {
        isShooting = false;
        Arrow.gameObject.SetActive(true);
    }
}