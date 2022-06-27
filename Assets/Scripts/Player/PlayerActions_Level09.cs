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
    [SerializeField] private bool isAiming = false;
    public Transform Arrow;
    public GameObject ArrowPrefab;
    public AudioManager AudioManager;
    public PlayerInput PlayerInput;

    private float x = 0;
    private float y = 0;

    public Controls ActionMap;
    public SceneChanger SceneChanger;

    private void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Level09.Shoot.performed += Shoot;
        ActionMap.Level09.Aim.performed += Aim;
        ActionMap.Level09.Aim.canceled += StopAim;
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (SceneChanger.NextScene || SceneChanger.GameOver)
        {
            ActionMap.Level09.Shoot.performed -= Shoot;
            ActionMap.Level09.Aim.performed -= Aim;
            ActionMap.Level09.Aim.canceled -= StopAim;
            ActionMap.Disable();

            if (SceneChanger.GameOver)
            {
                SceneChanger.SceneLoader.LoadGameOver();
            }
            else if (SceneChanger.NextScene)
            {
                SceneChanger.SceneLoader.LoadNextScene();
            }

            Destroy(this);
        }
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

    private void StopAim(InputAction.CallbackContext context)
    {
        Spine.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (!isShooting && isAiming)
        {
            isShooting = true;
            Animator.SetTrigger("shoot");
            Bow.SetTrigger("shoot");
            AudioManager.PlayClip(0);
        }  
    }

    private void ShootWithMouse()
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
