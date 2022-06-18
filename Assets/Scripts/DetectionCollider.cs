using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCollider : MonoBehaviour
{
    public ObservingGuard observingGuard;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            observingGuard.raiseDetection();
        }       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 3)
        {
            observingGuard.lowerDetection();
        }
    }

}
