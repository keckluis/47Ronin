using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public PathCreation.PathCreator pathCreator;
    AudioSource audioSource;
    public GuardPatrol guardPatrol;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //pathCreator = new PathCreation.PathCreator();
       
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.layer == 9)
            guardPatrol.addPointofIntrest(gameObject.transform);
            //GameObject objToSpawn = new GameObject("Cool GameObject made from Code");
        //objToSpawn.transform.position = gameObject.transform.position;
        audioSource.Play();
    }

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
}
