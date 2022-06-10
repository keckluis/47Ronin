using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour_Level7 : MonoBehaviour
{
    private Animator Animator;
    private bool isWalking = false;
    private bool actionPlaying = false;

    public GameObject Player;
    public int Health = 5;
    public float range = 4;
    public float speed = 0.03f;
    public Facing direction = Facing.RIGHT;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = transform.position.x - Player.transform.position.x;

        if(direction == Facing.RIGHT && distance > -range && distance <= 0)
        {
            Strike();
        }
        else if(direction == Facing.LEFT && distance < range && distance >= 0)
        {
            Strike();
        }

        float dir;
        if (direction == Facing.LEFT)
            dir = 0;
        else
            dir = 180;
        transform.localRotation = Quaternion.Euler(new Vector3(0, dir, 0));

        if (isWalking)
        {
            transform.Translate(-speed, 0, 0);
        }
    }

    public void Strike()
    {
        if (!actionPlaying)
        {
            actionPlaying = true;
            isWalking = false;
            Animator.SetTrigger("strike");
        }
    }

    public void ActionFinished()
    {
        actionPlaying = false;
        isWalking = false;
    }
}
