using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            //collision.transform.localScale = new Vector3(collision.transform.localScale.x * -1, collision.transform.localScale.y, collision.transform.localScale.z);
            Debug.Log("test");
                
             }

    }
}
