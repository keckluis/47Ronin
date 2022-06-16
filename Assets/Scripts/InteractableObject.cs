using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private SpriteRenderer sr;
    private GameObject copy;
    public float Outline = 0.05f;
    public bool OutlineVisible = false;
    public Transform Player;
    void Start()
    {
        copy = Instantiate(gameObject, transform);
        sr = copy.GetComponent<SpriteRenderer>();
        sr.color = Color.black;
        Destroy(copy.GetComponent<InteractableObject>());
        Destroy(copy.GetComponent<Collider2D>());
        copy.transform.localPosition = new Vector3(0, 0, 0);
        copy.transform.localEulerAngles = new Vector3(0, 0, 0);
        sr.sortingLayerName = "2";

        float a = transform.localScale.x / transform.localScale.y;

        if (a > 1)
            copy.transform.localScale = new Vector3(1 + Outline, 1 + (Outline * a), 1);
        else
        {
            a = transform.localScale.y / transform.localScale.x;
            copy.transform.localScale = new Vector3(1 + (Outline * a), 1 + Outline, 1);
        }
        copy.SetActive(false);  
    }

    void Update()
    {
        copy.SetActive(OutlineVisible); 

        //Set active when player is near
    }
}
