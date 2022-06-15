using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmBell : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("GAME OVER");
            Destroy(collision.gameObject);
        }   
    }
}
