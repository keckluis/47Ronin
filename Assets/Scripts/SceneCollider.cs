using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            GetComponent<SceneChanger>().NextScene = true;
    }
}
