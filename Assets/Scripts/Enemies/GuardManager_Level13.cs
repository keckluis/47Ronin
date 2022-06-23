using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager_Level13 : MonoBehaviour
{
    public Transform Player;
    public float VisibleDistance = 0;
    public List<EnemyBehaviour_Level13> Guards = new List<EnemyBehaviour_Level13>();
    private bool allKilled = false;
    public AudioManager AudioManager;
    public SceneChanger SceneChanger;

    void Update()
    {
        if (Guards.Count > 0 && !allKilled)
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
        else if (!allKilled)
        {
            Debug.Log("SUCCESS");
            allKilled = true;
            StartCoroutine(LevelEnd());
        }
    }

    public void RemoveGuard(EnemyBehaviour_Level13 Guard)
    {
        EnemyBehaviour_Level13 g;
        foreach (EnemyBehaviour_Level13 guard in Guards)
        {
            if (guard == Guard)
            {
                g = guard;
                Guards.Remove(g);
                return;
            }
        }
    }

    IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(3);
        SceneChanger.NextScene = true;
        AudioManager.gameObject.SetActive(false);
    }
}
