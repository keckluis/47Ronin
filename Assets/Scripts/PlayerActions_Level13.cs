using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions_Level13 : MonoBehaviour
{
    private Animator Animator;
    private bool isWalking = false;
    private bool actionPlaying = false;

    private float speed;
    public Facing direction = Facing.LEFT;

    public float Speed = 0.03f;

    public Collider2D Weapon;
    public GameObject Blood;
    private GameObject BloodHolder;

    public int Health = 100;

    public List<Transform> Ronins;
    public AudioManager AudioManager;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Weapon.enabled = false;
    }

    void Update()
    {
        Animator.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            transform.Translate(-speed, 0, 0);
            WeaponColliderOff();

            if (Ronins[0].position.x > transform.position.x + 4)
            {
                foreach (Transform ronin in Ronins)
                {
                    ronin.Translate(-speed, 0, 0);
                    ronin.GetComponent<Animator>().SetBool("isWalking", true);
                    float dir;
                    if (direction == Facing.LEFT)
                        dir = 0;
                    else
                        dir = 180;
                    ronin.localRotation = Quaternion.Euler(new Vector3(0, dir, 0));
                }
            } 
        }
        else
        {
            foreach (Transform ronin in Ronins)
            {
                ronin.GetComponent<Animator>().SetBool("isWalking", false);
            }
        }
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

    public void Action(string action)
    {
        if (!actionPlaying || action == "hit")
        {
            actionPlaying = true;
            isWalking = false;
            Animator.SetTrigger(action);
        }
    }

    public void ActionFinished()
    {
        actionPlaying = false;
        WeaponColliderOff();
    }

    public void WeaponColliderOn()
    {
        Weapon.enabled = true;
    }

    public void WeaponColliderOff()
    {
        Weapon.enabled = false;
    }

    public void Dodge()
    {
        AudioManager.PlayClip(2);
        WeaponColliderOff();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 50));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy_Weapon")
        {
            if (BloodHolder != null)
            {
                Destroy(BloodHolder);
                BloodHolder = null;
            }
            BloodHolder = Instantiate(Blood, transform.position, Quaternion.identity);
            Action("hit");
            AudioManager.PlayClip(1);
            
            Health -= 1;
            WeaponColliderOff();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 0));

            if (Health <= 0)
                GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        if (GameObject.Find("SceneLoader"))
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadGameOver();
        }
    }
}
