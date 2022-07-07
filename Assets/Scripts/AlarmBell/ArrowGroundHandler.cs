using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGroundHandler : MonoBehaviour
{
    public Transform GroundArrowParent;
    public int MaxArrows = 25;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            Destroy(collision.gameObject.GetComponent<Collider2D>());
            collision.transform.parent = GroundArrowParent;
        }
    }

    private void Update()
    {
        if (GroundArrowParent.childCount > MaxArrows)
        {
            Destroy(GroundArrowParent.GetChild(0).gameObject);
        }
    }
}
