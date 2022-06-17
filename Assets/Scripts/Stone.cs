using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    AudioSource audioSource;
    public GuardPatrol guardPatrol;
    public StandingGuard standingGuard;
    private bool hearedSomething;
    // Start is called before the first frame update
    void Start()
    {
        GameObject a = GameObject.Find("Audio");
        audioSource = a.GetComponent<AudioSource>();
        //pathCreator = new PathCreation.PathCreator();
       
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.layer == 9)
        {
            if (hearedSomething)
            {
                //if (collision.gameObject.transform.parent.GetComponent<GuardPatrol>() != null)
                    guardPatrol.addPointofIntrest(gameObject.transform.position);
               // else if (collision.gameObject.transform.parent.GetComponent<StandingGuard>() != null)
                  //  standingGuard = collision.gameObject.transform.parent.GetComponent<StandingGuard>();
            }
            audioSource.Play();
            hearedSomething = false;
      
            Destroy(gameObject);
        }
            //GameObject objToSpawn = new GameObject("Cool GameObject made from Code");
        //objToSpawn.transform.position = gameObject.transform.position;
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            hearedSomething = true;
           // if(collision.gameObject.transform.parent.GetComponent<GuardPatrol>() != null)
                guardPatrol = collision.gameObject.transform.parent.GetComponent<GuardPatrol>();
           // else if (collision.gameObject.transform.parent.GetComponent<StandingGuard>() != null)
             //   standingGuard = collision.gameObject.transform.parent.GetComponent<StandingGuard>();
                
        }

    }
    
    /*
    void addSuspiciousPos(Vector3 SuspiciosPoint)
    {
        int length = (int)(pathCreator.path.length - 1);
        Vector3 Start = pathCreator.bezierPath.GetPoint(0);
        Vector3 End = pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1);
        if (Mathf.Abs(SuspiciosPoint.x) - (Mathf.Abs(Start.x)) < (Mathf.Abs(SuspiciosPoint.x) - (Mathf.Abs(End.x))))
        {
            pathCreator.bezierPath.AddSegmentToEnd(SuspiciosPoint);
            //pathCreator.bezierPath.SetPoint(pathCreator.path.NumPoints - 0, SuspiciosPoint);
        }

        else if (Mathf.Abs(SuspiciosPoint.x) - (Mathf.Abs(Start.x)) > (Mathf.Abs(SuspiciosPoint.x) - (Mathf.Abs(End.x))))
        {
            pathCreator.bezierPath.AddSegmentToStart(SuspiciosPoint);
            //pathCreator.bezierPath.SetPoint(0, SuspiciosPoint);
        }
    }
    */
}
