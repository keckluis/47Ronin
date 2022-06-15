using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmBell : MonoBehaviour
{
    public AudioManager AudioManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.GetComponent<Enemy_Behaviour_Level10>());
            Destroy(collision.GetComponent<Animator>());
            AudioManager.PlayClip(1);
            StartCoroutine(GameOver());
        }   
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("GAME OVER");
        if (GameObject.Find("SceneLoader"))
        {
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadGameOver();
        }
    }
}
