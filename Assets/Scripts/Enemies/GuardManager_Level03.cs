using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardManager_Level03 : MonoBehaviour
{
    public int minNum;
    public int maxNum;
    public bool GroundFloor = false;
    float x;
    float z;
    float newPos;

    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        z = transform.position.z;
        newPos = transform.position.y;
        StartCoroutine(ChangeFloor());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator ChangeFloor() {       
        int rand_num = Random.Range(minNum, maxNum); 
        yield return new WaitForSeconds(rand_num);
      
        switch (transform.position.y) {
            case 13:
                newPos = 3;
                GroundFloor = false;
                break;
            case 3:
                if(!GroundFloor) newPos = -7;
                else newPos = 13;
                break;
            case -7:
                newPos = 3;
                GroundFloor = true;
                break;
        }

        transform.position = new Vector3(x, newPos, z);
        Debug.Log("Changing Floor!");       
       
        StartCoroutine(ChangeFloor());
    }
}