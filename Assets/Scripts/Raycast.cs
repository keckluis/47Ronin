using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{ 
    [SerializeField] float fieldOfView = 90f;
    [SerializeField] int rayCount = 2;
    [SerializeField] float viewDistance = 8f;

    private float parentDirection;
    private LayerMask layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Player", "Obstruction");
    }

    private void Update()
    {
        CastRays();
    }


    private static Vector3 GetVectorFromAngle(float angle) 
    {    
        float angleRadius = angle * (Mathf.PI / 180f);
        
        return new Vector3(Mathf.Cos(angleRadius), Mathf.Sin(angleRadius));
    }

    private void CastRays()
    {
        parentDirection = this.transform.parent.transform.localScale.x;
        
        Vector3 origin = this.transform.position;
        float angleIncrement = fieldOfView / rayCount;
        float angle;

        if(parentDirection == -1f) {
            angle = 290f;
        } else {
            angle = 0f;
        }
         
        for (int i = 0; i <= rayCount; i++) {
            Vector3 angleVector = GetVectorFromAngle(angle);

            RaycastHit2D hit = Physics2D.Raycast(origin, angleVector, viewDistance, layerMask);
            Debug.DrawRay(origin, angleVector * viewDistance, Color.green);
            
            if (hit.collider != null) {
                if (hit.collider.gameObject.CompareTag("Player")){
                    print("player hit!");
                } else {
                    // print("obstruction hit!");
                }
            }
        
            angle -=angleIncrement;
        }
    }
}