using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeVase : MonoBehaviour
{
    public GameObject SmokeCload;
    public GameObject DetectionCollider;
    AudioSource smashSound;

    private void Start()
    {
        smashSound = GameObject.Find("AudioVase").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            smashSound.timeSamples = 6;
            smashSound.Play();
            Destroy(DetectionCollider);
            Vector3 SmokeOffset = new Vector3(transform.position.x - 0.15f, transform.position.y + 1.239f, transform.position.z);
            GameObject SmokeCloadInst = GameObject.Instantiate(SmokeCload, SmokeOffset, transform.rotation);           
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
