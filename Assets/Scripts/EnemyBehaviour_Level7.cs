using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour_Level7 : MonoBehaviour
{
    private Animator Animator;
    private bool isWalking = false;
    private bool actionPlaying = false;
    public bool isCoolingDown = false;

    public GameObject Player;
    public int Health = 100;
    public float Range = 4;
    public float Cooldown = 1f;
    public float Speed = 0.03f;
    public Facing direction = Facing.RIGHT;

    public GameObject Blood;
    private GameObject BloodHolder;

    public GameObject Dead;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = transform.position.x - Player.transform.position.x;

        if (direction == Facing.RIGHT && distance > -Range && distance <= 0 && !isCoolingDown)
        {
            Action("strike");
        }
        else if (direction == Facing.LEFT && distance < Range && distance >= 0 && !isCoolingDown)
        {
            Action("strike");
        }

        float dir;
        if (direction == Facing.LEFT)
            dir = 0;
        else
            dir = 180;
        transform.localRotation = Quaternion.Euler(new Vector3(0, dir, 0));

        if (isWalking)
        {
            transform.Translate(-Speed, 0, 0);
        }
    }

    public void Action(string action)
    {
        if (!actionPlaying || action == "hit" || action == "die")
        {
            isCoolingDown = true;
            actionPlaying = true;
            isWalking = false;
            Animator.SetTrigger(action);
            StartCoroutine(WaitForCooldown());
        }
    }

    public void ActionFinished()
    {
        actionPlaying = false;
        isWalking = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player_Weapon")
        {          
            if (BloodHolder != null)
            {
                Destroy(BloodHolder);
                BloodHolder = null;
            }
            BloodHolder = Instantiate(Blood, transform.position, Quaternion.identity);
            Action("hit");
            Health -= 1;

            if (Health <= 0)
            {
                Action("die");
                return;
            }               

            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000, 750), ForceMode2D.Force);
        }
    }

    public void Remove()
    {
        GameObject dead = Instantiate(Dead, transform.position, Quaternion.Euler(0, 180, -90));
        SpriteRenderer[] rends = dead.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].sortingLayerName = "5";
        }
        Destroy(BloodHolder);
        Destroy(gameObject);
    }

    IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        isCoolingDown = false;
    }
}
