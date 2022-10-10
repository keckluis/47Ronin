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

    public int StartHealth = 10;
    public int Health;
    public Transform HealthBarFiller;

    public List<Transform> Ronins;
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
        ActionMap.Level0713.Block.performed += Dodge;

        Health = StartHealth;
    }

    private void OnDestroy()
    {
        ActionMap.Level0713.Walk.started -= Walk;
        ActionMap.Level0713.Walk.canceled -= StopWalking;
        ActionMap.Level0713.Strike.performed -= Strike;
        ActionMap.Level0713.Block.performed -= Dodge;
        ActionMap.Disable();
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
                ActionMap.Level0713.Block.performed -= Dodge;
                ActionMap.Disable();

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
            }

            if (direction == Facing.LEFT)
                HealthBarFiller.parent.localEulerAngles = Vector3.zero;
            else
                HealthBarFiller.parent.localEulerAngles = new Vector3(0, 180, 0);

            HealthBarFiller.localScale = new Vector3((1 / (float)StartHealth) * Health, 1, 1);
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
            //WeaponColliderOn();
            actionPlaying = true;
            isWalking = false;
            Animator.SetTrigger("strike");
        }
    }

    public void Dodge(InputAction.CallbackContext ctx)
    {
        if (!actionPlaying)
        {
            AudioManager.PlayClip(2);
            WeaponColliderOff();

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(400, 100));

            actionPlaying = true;
            isWalking = false;
            Animator.SetTrigger("dodge");
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

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        SceneChanger.GameOver = true;
        
        AudioManager.gameObject.SetActive(false);
    }
}
