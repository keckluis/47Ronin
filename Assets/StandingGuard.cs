using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingGuard : MonoBehaviour
{
    bool suspicios = false;
    Vector3 PointOfInterest;
    public GameObject detectionStatusIcon;
    public GameObject TriggerPrefab;
    public bool patrolGuard = true;
    private bool returning = false;

    public Vector2 GuardingPos;
    public Vector3 GuardingDirection;


    public bool detected { get; set; } = false;

    private Animator Animator;

    private void Start()
    {

        if (GetComponent<Animator>() != null)
        {
            Animator = GetComponent<Animator>();
            Animator.SetBool("isWalking", true);
        }
        GuardingPos = transform.localPosition;
        GuardingDirection = transform.localEulerAngles;
    }
    void FixedUpdate()
    {
        if (suspicios)
            moveToSuspicion();
        else if (!patrolGuard && returning)
            returnToGuardPost();
    }

    private void moveToSuspicion()
    {
        transform.position = Vector2.MoveTowards(transform.position, PointOfInterest, 0.051f);
        lookAtDirection(transform.position, PointOfInterest);
        transform.Rotate(new Vector3(0, 180, 0));
    }

    private void returnToGuardPost()
    {

        transform.position = Vector2.MoveTowards(transform.position, GuardingPos, 0.05f);
        lookAtDirection(transform.position, GuardingPos);
        transform.Rotate(new Vector3(0, 180, 0));
        if (Vector3.Distance(transform.position, GuardingPos) < .001f)
        returning = false;
    }

    private void lookAtDirection(Vector3 guardPos, Vector3 TargetPos)
    {
        if (TargetPos.x > guardPos.x)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 12)
        {
            StartCoroutine(Wait(2));
            suspicios = false;
            Destroy(other.gameObject);

            //lookAtDirection(transform.position, PointOfInterest.position);

        }

    }

    public void addPointofIntrest(Vector3 PoI)
    {
        if (!detected)
        {
            suspicios = true;
            PointOfInterest = PoI;
            GameObject trigger =
                Instantiate(TriggerPrefab.gameObject, PointOfInterest, Quaternion.identity);
            detectionStatusIcon.SetActive(true);


        }
    }

    IEnumerator Wait(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        detectionStatusIcon.SetActive(false);
        returning = true;
    }
}
