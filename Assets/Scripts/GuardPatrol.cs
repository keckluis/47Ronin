using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public Transform[] PatrolTargets;
    // Update is called once per frame
    public int index = 0;
    bool suspicios = false;
    bool patroling = true;
    Vector3 PointOfInterest;
    public GameObject detectionStatusIcon;
    public GameObject TriggerPrefab;
    

    public bool detected { get; set; } = false;

    private Animator Animator;

    private void Start()
    {
        lookAtDirection(transform.position, PatrolTargets[index].position);

        if (GetComponent<Animator>() != null)
        {
            Animator = GetComponent<Animator>();
            Animator.SetBool("isWalking", true);
        }         
    }
    void FixedUpdate()
    {
        if (suspicios)
        { 
            transform.position = Vector2.MoveTowards(transform.position, PointOfInterest, 0.051f);
            lookAtDirection(transform.position, PointOfInterest);
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

    private void findTarget()
    {
        if (Vector3.Distance(transform.position, PatrolTargets[0].position) > Vector3.Distance(transform.position, PatrolTargets[1].position))
        {
            index = 0;
            PatrolTargets[0].GetComponent<ColliderStatus>().active = true;
        }
        else
        {
            index = 1;
            PatrolTargets[1].GetComponent<ColliderStatus>().active = true;
        }
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
            if (other.GetComponent<ColliderStatus>().active)
            {
                PatrolTargets[index].GetComponent<ColliderStatus>().active = false;
                //lookAtDirection(transform.position, PatrolTargets[index].position);
                index++;
                if (index >= PatrolTargets.Length)
                    index = 0;
                PatrolTargets[index].GetComponent<ColliderStatus>().active = true;
            }
        }

        if (other.gameObject.layer == 12)
        {
            StartCoroutine(Wait(2));
            suspicios = false;
            index = 0;
            patroling = false;
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
        findTarget();
        patroling = true;
        detectionStatusIcon.SetActive(false);
}

    private void LookAtDirection(Transform Target)
    {
       
    }

}
