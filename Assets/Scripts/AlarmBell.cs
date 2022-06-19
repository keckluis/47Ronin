using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmBell : MonoBehaviour
{
    public AudioManager AudioManager;
    bool bellReached = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !bellReached)
        {
            Destroy(collision.GetComponent<EnemyBehaviour_Level09>());
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
        bellReached = true;
        AudioManager.gameObject.SetActive(false);
    }
}
