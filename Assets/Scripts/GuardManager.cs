using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    public Transform Player;
    public float VisibleDistance = 0;
    public List<EnemyBehaviour_Level07> Guards = new List<EnemyBehaviour_Level07>();

    void Update()
    {
        if(Guards.Count > 0)
        {
            if (Player.position.x - Guards[0].transform.position.x < VisibleDistance)
            {
                if (!Guards[0].actionPlaying)
                {
                    Guards[0].isWalking = true;
                }

                if (Player.position.x - Guards[0].transform.position.x < 3)
                {
                    Guards[0].isWalking = false;
                }
            }
            else
                Guards[0].isWalking = false;
        }
        else
        {
            Debug.Log("SUCCESS");
        }
    }

    public void RemoveGuard(EnemyBehaviour_Level07 Guard)
    {
        EnemyBehaviour_Level07 g;
        foreach(EnemyBehaviour_Level07 guard in Guards)
        {
            if(guard == Guard)
            {
                g = guard;
                Guards.Remove(g);
                return;
            }
        }            
    }
}
