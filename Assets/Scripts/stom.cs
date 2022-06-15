using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stom : MonoBehaviour
{
    PathCreation.PathCreator pathCreator;
    // Update is called once per frame
    void addSuspiciousPos(Vector3 SuspiciosPoint)
    {
        Vector3 Start = pathCreator.bezierPath.GetPoint((int)pathCreator.path.length - 1);
        Vector3 End = pathCreator.path.GetPoint((int)pathCreator.path.length - 1);
        if((SuspiciosPoint - Start).sqrMagnitude < (SuspiciosPoint - End).sqrMagnitude)

        pathCreator.bezierPath.AddSegmentToEnd(SuspiciosPoint);

        if ((SuspiciosPoint - Start).sqrMagnitude > (SuspiciosPoint - End).sqrMagnitude)

            pathCreator.bezierPath.AddSegmentToStart(SuspiciosPoint);
    }
}
