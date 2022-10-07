using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> GuardPrefabs;
    public int GuardTotal = 15;

    public int SpawnedGuards = 0;

    private bool allKilled = false;

    public SceneChanger SceneChanger;

    bool justSpawned = true;
    bool levelStarted = false;

    void Start()
    {
        StartCoroutine(LevelStart());
    }

    void Update()
    {
        if (transform.childCount < 6 && levelStarted)
        {
            if (!justSpawned || transform.childCount == 0)
            {
                if (SpawnedGuards >= GuardTotal && !allKilled && transform.childCount == 0)
                {
                    Debug.Log("SUCCESS");
                    allKilled = true;
                    StartCoroutine(LevelEnd());
                }

                if (!allKilled && SpawnedGuards < GuardTotal)
                {
                    int groupSize = Random.Range(1, 2);
                    int speed = Random.Range(15, 25);
                    SpawnedGuards += groupSize;

                    for (int i = 0; i < groupSize; i++)
                    {
                        SpawnGuard(i * 1, speed);
                    }
                    justSpawned = true;
                    StartCoroutine(SpawnCooldown());
                }
            }          
        }
    }

    private void SpawnGuard(float dist, float speed)
    {
        int guardColor = Random.Range(0, GuardPrefabs.Count);
        GameObject guard = Instantiate(GuardPrefabs[guardColor], this.transform);
        guard.transform.Translate(new Vector3(dist, 0, 0));
        guard.GetComponent<EnemyBehaviour_Level09>().Speed = speed;
    }

    IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(3);
        SceneChanger.NextScene = true;
    }
    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(10);
        justSpawned = false;
    }

    IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(5);
        SpawnGuard(0, 20);
        StartCoroutine(SpawnCooldown());
        levelStarted = true;
    }
}
