using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator animator;
    public Transform cam;
    public float speed;

    private void Start()
    {
        animator.speed = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            animator.speed = 1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            animator.speed = 1;
        }
        if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
            animator.speed = 0;
        if (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A))
            animator.speed = 0;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        cam.position = new Vector3(this.transform.position.x, cam.position.y, cam.position.z);
    }
}
