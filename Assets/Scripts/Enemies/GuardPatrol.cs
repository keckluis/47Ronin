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
    public GameObject SpawnPoint;


    public bool patrolGuard = true;
    private bool returning = false;

    private GameObject SpawnpointInst;

    public Vector2 GuardingPos;
    public Vector3 GuardingDirection;
    

    public bool detected { get; set; } = false;

    private Animator Animator;

    private void Start()
    {
        if (patrolGuard) 
        {
            lookAtDirection(transform.position, PatrolTargets[index].position);
            PatrolTargets[0].GetComponent<ColliderStatus>().active = true;
        }
        else
        {
            SpawnpointInst = Instantiate(SpawnPoint, transform.position, Quaternion.identity);
        }
        if (GetComponent<Animator>() != null)
        {
            Animator = GetComponent<Animator>();
            Animator.SetBool("isWalking", true);

            if(!patrolGuard)
                Animator.SetBool("isWalking", false);
        }
       
            
        GuardingPos = transform.position;
        GuardingDirection = transform.eulerAngles;
    }
    void FixedUpdate()
    {
        if (suspicios && !returning)
            moveToSuspicion();
        else if (patroling && patrolGuard)
            Patrol();
        else if (!patrolGuard && returning)
            returnToGuardPost();
    }

    private void moveToSuspicion()
    {
        transform.position = Vector2.MoveTowards(transform.position, PointOfInterest, 0.051f);
        lookAtDirection(transform.position, PointOfInterest);
        transform.Rotate(new Vector3(0, 180, 0));
        Animator.SetBool("isWalking", true);
    }

    private void Patrol()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, PatrolTargets[index].position, 0.05f);
        lookAtDirection(transform.position, PatrolTargets[index].position);
        transform.Rotate(new Vector3(0, 180, 0));
    }
    private void returnToGuardPost()
    {
    
        transform.position = Vector2.MoveTowards(transform.position, SpawnpointInst.transform.position, 0.05f);
        lookAtDirection(transform.position, GuardingPos);
        transform.Rotate(new Vector3(0, 180, 0));
        
    }

    private void findTarget()
    {
        if (patrolGuard)
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
        Debug.Log(other.gameObject);
        if (other.gameObject.layer == 11)
        {
            Debug.Log("s");
            if (other.GetComponent<ColliderStatus>().active)
            {
                Debug.Log("s1");
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
            Animator.SetBool("isWalking", false);
            suspicios = false;
            
            index = 0;
            patroling = false;
            Debug.Log("sre");
            Destroy(other.gameObject);
            
            //lookAtDirection(transform.position, PointOfInterest.position);

        }
        if(other.gameObject.layer == 15 && returning)
        {
            returning = false;
            lookAtDirection(transform.position, PointOfInterest);
            transform.Rotate(new Vector3(0, 180, 0));
            Animator.SetBool("isWalking", false);
        }

    }

    public void addPointofIntrest(Vector3 PoI)
    {
        if (!detected && Vector3.Distance(transform.position, PoI) > 2)
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
        returning = true;
        Animator.SetBool("isWalking", true);

    }
}
