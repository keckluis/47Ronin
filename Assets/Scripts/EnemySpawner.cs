using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject GuardPrefab;

    void Start()
    {
        SpawnGuard(0);
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            int groupSize = Random.Range(1, 4);

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
