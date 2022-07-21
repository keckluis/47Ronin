using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardManager_Level03 : MonoBehaviour
{
    public int minNum;
    public int maxNum;
    public bool GroundFloor = false;
    public float PlayerRadius = 10;
    float x;
    float z;
    float newPos;
    Transform Player;
    float CurrentDistance;

    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        z = transform.position.z;
        newPos = transform.position.y;
        StartCoroutine(ChangeFloor());

        Player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get distance of x-coordinates of player and guard
        CurrentDistance = Vector3.Distance(Player.position, new Vector3(transform.Find("Guard").position.x, Player.position.y, Player.position.z));
        // Debug.Log(CurrentDistance);
   }

    IEnumerator ChangeFloor() {       
        int rand_num = Random.Range(minNum, maxNum); 
        yield return new WaitForSeconds(rand_num);

        // only change floors when outside of player view
        if (CurrentDistance > PlayerRadius) {
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
        }
        StartCoroutine(ChangeFloor());
    }
}