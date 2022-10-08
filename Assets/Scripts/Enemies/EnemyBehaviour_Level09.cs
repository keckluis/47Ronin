using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour_Level09 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Speed = 1;
    public GameObject Dead;
    private AudioManager AudioManager;
    private Animator Animator;
    private Transform ArrowTransform;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        if (GameObject.Find("AudioManager"))
            AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(-0.001f * Speed, 0, 0));
    }

    public void Die()
    {
        GameObject dead = Instantiate(Dead, transform.position, Quaternion.Euler(0, 180, -90));
        SpriteRenderer[] rends = dead.GetComponentsInChildren<SpriteRenderer>();
        dead.transform.localScale = new Vector3(0.25f, 0.25f, 0);
        ArrowTransform.parent = dead.transform.GetChild(7);
        ArrowTransform.localPosition = new Vector3(0, 0.5f, 0);
        Destroy(ArrowTransform.gameObject.GetComponent<Rigidbody2D>());
        Destroy(ArrowTransform.gameObject.GetComponent<Collider2D>());
        ArrowTransform.GetComponent<SpriteRenderer>().enabled = true;

        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].sortingLayerName = "2";
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            AudioManager.PlayClip(2);
            ArrowTransform = collision.transform;
            Destroy(collision.GetComponent<Collider2D>());
            collision.GetComponent<SpriteRenderer>().enabled = false;
            Animator.SetTrigger("die");
        }
    }
}
