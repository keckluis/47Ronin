using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRaycast : MonoBehaviour
{
    // Float a rigidbody object a set distance above a surface.

    public Transform startPoint;
    public LayerMask VisionLayer;
    Rigidbody2D rb2D;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //VisionLayer += LayerMask.GetMask("Player");
        //VisionLayer += LayerMask.GetMask("Obstruction");
    }

    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(startPoint.position, transform.right, Mathf.Infinity, VisionLayer);       
        // If it hits something...
        if (hit.collider != null)   
        {
            
        
            if (!hit.collider.IsTouchingLayers(LayerMask.GetMask("Obstruction")))
            {
                if(hit.collider.gameObject.layer == 3)
                Debug.Log(hit.collider.gameObject);
            }
            
        }
    }
}

