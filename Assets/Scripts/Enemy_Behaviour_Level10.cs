using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour_Level10 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Speed = 1;
    public GameObject Dead;
    private AudioManager AudioManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        transform.Translate(new Vector3(-0.001f * Speed, 0, 0));
    }

    private void Die(Transform arrow)
    {
        GameObject dead = Instantiate(Dead, transform.position, Quaternion.Euler(0, 180, -90));
        SpriteRenderer[] rends = dead.GetComponentsInChildren<SpriteRenderer>();
        dead.transform.localScale = new Vector3(0.25f, 0.25f, 0);
        arrow.parent = dead.transform.GetChild(7);
        arrow.localPosition = new Vector3(0, 0.5f, 0);
        Destroy(arrow.gameObject.GetComponent<Rigidbody2D>());
        Destroy(arrow.gameObject.GetComponent<Collider2D>());

        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].sortingLayerName = "3";
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            AudioManager.PlayClip(2);
            Die(collision.transform);
        }
    }
}
