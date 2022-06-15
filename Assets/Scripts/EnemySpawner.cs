using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject GuardPrefab;
    public int GuardTotal = 10;

    private int SpawnedGuards = 0;

    void Start()
    {
        SpawnGuard(0);
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            if (SpawnedGuards >= GuardTotal)
            {
                Debug.Log("SUCCESS");
                if (GameObject.Find("SceneLoader"))
                {
                    GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadNextScene();
                }
                Destroy(gameObject);
            }

            int groupSize = Random.Range(1, 4);
            SpawnedGuards += groupSize;

            for (int i = 0; i < groupSize; i++)
            {
                SpawnGuard(i * 1);
            }     
        }
    }

    private void SpawnGuard(float dist)
    {
        GameObject guard = Instantiate(GuardPrefab, this.transform);
        guard.transform.Translate(new Vector3(dist, 0, 0));
    }
}
