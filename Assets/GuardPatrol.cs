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
    void FixedUpdate()
    {
        if (suspicios)
            transform.position = Vector2.MoveTowards(transform.position, PointOfInterest.position, 0.051f);
        else if (patroling)
            Patrol();
    }

    private void Patrol()
    {
            transform.position = Vector2.MoveTowards(transform.position, PatrolTargets[index].position, 0.051f);     
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 11)
        {
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
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(waitTime);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        patroling = true;
    }
}
