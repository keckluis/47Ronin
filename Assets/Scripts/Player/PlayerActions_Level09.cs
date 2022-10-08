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
    public Transform Arrow;
    public GameObject ArrowPrefab;
    public AudioManager AudioManager;
    public float LaunchForce = 750;

    private float x = 0;
    private float y = 0;
    private Vector2 shotDirection;

    public Controls ActionMap;
    public SceneChanger SceneChanger;

    private float keyboardAim = 0;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;

    private void Awake()
    {
        ActionMap = new Controls();

        ActionMap.Enable();
        ActionMap.Level09.Shoot.performed += Shoot;
        ActionMap.Level09.Aim.performed += Aim;
        ActionMap.Level09.AimKeyboard.started += AimKeyboard;
        ActionMap.Level09.AimKeyboard.canceled += StopAimKeyboard;
        ActionMap.Level09.Aim.canceled += StopAim;
    }

    private void OnDestroy()
    {
        ActionMap.Level09.Shoot.performed -= Shoot;
        ActionMap.Level09.Aim.performed -= Aim;
        ActionMap.Level09.AimKeyboard.started -= AimKeyboard;
        ActionMap.Level09.AimKeyboard.canceled -= StopAimKeyboard;
        ActionMap.Level09.Aim.canceled -= StopAim;
        ActionMap.Disable();
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();

        points = new GameObject[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, Arrow.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (SceneChanger.NextScene || SceneChanger.GameOver)
        {
            ActionMap.Level09.Shoot.performed -= Shoot;
            ActionMap.Level09.Aim.performed -= Aim;
            ActionMap.Level09.AimKeyboard.started -= AimKeyboard;
            ActionMap.Level09.AimKeyboard.canceled -= StopAimKeyboard;
            ActionMap.Level09.Aim.canceled -= StopAim;
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

            Destroy(this);
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition((i + 1) * spaceBetweenPoints);
        }
    }

    private void FixedUpdate()
    {
        float z = Spine.localEulerAngles.z;
        if (z > 91)
            z = z - 360;

        if (keyboardAim > 0 && z > -90)
            z -= 1.5f;
        else if (keyboardAim < 0 && z < 90)
            z += 1.5f;

        Spine.localEulerAngles = new Vector3(0, 0, z);
        x = (Mathf.Abs(z) / 90) - 1;
        y = -(z / 90);

        shotDirection = new Vector2(x, y);
    }

    public void AimKeyboard(InputAction.CallbackContext context)
    {
        keyboardAim = context.ReadValue<float>();
    }

    public void StopAimKeyboard(InputAction.CallbackContext context)
    {
        keyboardAim = 0;
    }

    public void Aim(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        x = input.x;
        y = input.y;

        if (x > 0)
            return;

        if (x == 0 && y == 0)
        {
            Spine.localRotation = Quaternion.Euler(0, 0, 0);
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
        if (!isShooting)
        {
            isShooting = true;
            Animator.SetTrigger("shoot");
            Bow.SetTrigger("shoot");
            AudioManager.PlayClip(0);

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].SetActive(false);
            }
        }  
    }

    public void ArrowFlight()
    {
        Arrow.gameObject.SetActive(false);
        GameObject arrow = Instantiate(ArrowPrefab, Arrow.position, Arrow.rotation);

        arrow.GetComponent<Rigidbody2D>().velocity = Spine.up * LaunchForce;
    }

    public void ShotDone()
    {
        isShooting = false;
        Arrow.gameObject.SetActive(true);
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].SetActive(true);
        }
    }

    Vector3 PointPosition(float t)
    {
        Vector2 position = (Vector2)Arrow.position + (shotDirection.normalized * LaunchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return new Vector3(position.x, position.y, -1.5f);
    }
}
