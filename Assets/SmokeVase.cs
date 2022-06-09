using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeVase : MonoBehaviour
{
    public GameObject SmokeCload;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            GameObject SmokeCloadInst = GameObject.Instantiate(SmokeCload, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
