using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public Transform[] PatrolTargets;
    // Update is called once per frame
    int index = 0;
    bool suspicios = false;
    bool patroling = true;
    Transform PointOfInterest;

    public GameObject TriggerPrefab;

    private Animator Animator;

    private void Start()
    {
        lookAtDirection(transform.position, PatrolTargets[index].position);
        Animator = GetComponent<Animator>();
        Animator.SetBool("isWalking", true);
    }
    void FixedUpdate()
    {
        if (suspicios)
        { 
            transform.position = Vector2.MoveTowards(transform.position, PointOfInterest.position, 0.051f);
            lookAtDirection(transform.position, PointOfInterest.position);
            transform.Rotate(new Vector3(0, 180, 0));
        }
            else if (patroling)
            Patrol();


           
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, PatrolTargets[index].position, 0.05f);
        lookAtDirection(transform.position, PatrolTargets[index].position);
        transform.Rotate(new Vector3(0, 180, 0));
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
        if (other.gameObject.layer == 11)
        {
            //lookAtDirection(transform.position, PatrolTargets[index].position);
            index++;        
            if (index >= PatrolTargets.Length)
                index = 0;
        }
        if (other.gameObject.layer == 12)
        {
            StartCoroutine(Wait(2));
            suspicios = false;
            index = 0;
            patroling = false;
            //lookAtDirection(transform.position, PointOfInterest.position);

        }

    }

    public void addPointofIntrest(Transform PoI)
    {
        suspicios = true;
        PointOfInterest = PoI;
        GameObject trigger = 
            Instantiate(TriggerPrefab.gameObject, PointOfInterest.transform.position, PointOfInterest.transform.rotation);
       
    }

    IEnumerator Wait(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        patroling = true;
    }

    private void LookAtDirection(Transform Target)
    {
       
    }

}
