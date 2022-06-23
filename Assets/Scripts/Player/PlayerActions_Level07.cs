using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Facing
{
    LEFT,
    RIGHT
}
public class PlayerActions_Level07 : MonoBehaviour
{
    private Animator Animator;
    private bool isWalking = false;
    private bool actionPlaying = false;
    public bool isBlocking = false;
   
    private float speed;
    public Facing direction = Facing.LEFT;

    public float Speed = 0.03f;

    public Collider2D Weapon;
    public GameObject Blood;
    private GameObject BloodHolder;

    public int Health = 100;
    public AudioManager AudioManager;

    bool dead = false;

    public Controls ActionMap;
    public SceneChanger SceneChanger;

    private void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Level0713.Walk.started += Walk;
        ActionMap.Level0713.Walk.canceled += StopWalking;
        ActionMap.Level0713.Strike.performed += Strike;
        ActionMap.Level0713.Block.performed += Block;
    }

    void Start()
    {
        Animator = GetComponent<Animator>();
        Weapon.enabled = false;
    }

    void FixedUpdate()
    {
        Animator.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            transform.Translate(-speed, 0, 0);
            WeaponColliderOff();
        }
    }

    private void Update()
    {
        if (!dead)
        {
            if (SceneChanger.NextScene || SceneChanger.GameOver)
            {
                dead = true;
                ActionMap.Level0713.Walk.started -= Walk;
                ActionMap.Level0713.Walk.canceled -= StopWalking;
                ActionMap.Level0713.Strike.performed -= Strike;
                ActionMap.Level0713.Block.performed -= Block;
                ActionMap.Disable();

                if (SceneChanger.GameOver)
                {
                    SceneChanger.SceneLoader.LoadGameOver();
                }
                else if (SceneChanger.NextScene)
                {
                    SceneChanger.SceneLoader.LoadNextScene();
                }
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

    public void StopWalking(InputAction.CallbackContext ctx)
    {
        isWalking = false;
    }

    public void Strike(InputAction.CallbackContext ctx)
    {
        if (!actionPlaying)
        {
            WeaponColliderOn();
            AudioManager.PlayClip(0);
            actionPlaying = true;
            isWalking = false;
            Animator.SetTrigger("strike");
        }   
    }

    public void Block(InputAction.CallbackContext ctx)
    {
        if (!actionPlaying)
        {
            StartBlock();
            WeaponColliderOff();
            actionPlaying = true;
            isWalking = false;
            Animator.SetTrigger("block");
        }
    }

    public void Hit()
    {
        WeaponColliderOff();
        actionPlaying = true;
        isWalking = false;
        Animator.SetTrigger("hit");
    }

    public void ActionFinished()
    {
       actionPlaying = false;
    }

    public void WeaponColliderOn()
    {
        Weapon.enabled = true;
    }

    public void WeaponColliderOff()
    {
        Weapon.enabled = false;
    }

    public void StartBlock()
    {
        isBlocking = true;
    }
    public void EndBlock()
    {
        isBlocking = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy_Weapon")
        {
            if (isBlocking)
            {
                Transform t = collider.transform;

                while (t.parent != null)
                {
                    if (t.parent.tag == "Enemy")
                    {
                        t.parent.GetComponent<EnemyBehaviour_Level07>().Action("hit");
                        AudioManager.PlayClip(2);
                        t.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        t.parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(-20000, 0));
                        return;
                    }
                    t = t.parent;
                }
            }
            else
            {
                if (BloodHolder != null)
                {
                    Destroy(BloodHolder);
                    BloodHolder = null;
                }
                BloodHolder = Instantiate(Blood, transform.position, Quaternion.identity);
                Hit();
                
                AudioManager.PlayClip(1);
                Health -= 1;
                WeaponColliderOff();
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 0));

                if (Health <= 0 && !dead)
                    GameOver();
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        SceneChanger.GameOver = true;
        AudioManager.gameObject.SetActive(false);
    }
}
