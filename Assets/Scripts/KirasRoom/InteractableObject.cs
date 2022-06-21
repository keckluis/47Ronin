using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    private SpriteRenderer sr;
    private GameObject copy;
    public float Outline = 0.05f;
    public bool OutlineVisible = false;
    public Transform Player;
    public float Radius = 10;
    public bool Interacted = false;

    void Start()
    {
        copy = Instantiate(gameObject, transform);
        sr = copy.GetComponent<SpriteRenderer>();
        sr.color = Color.black;
        Destroy(copy.GetComponent<InteractableObject>());
        Destroy(copy.GetComponent<Collider2D>());
        copy.transform.localPosition = new Vector3(0, 0, 0);
        copy.transform.localEulerAngles = new Vector3(0, 0, 0);
        sr.sortingLayerName = "3";

        copy.transform.localScale = new Vector3(1 + Outline, 1 + Outline, 1);
        copy.SetActive(false);  
    }

    void Update()
    {
        if((Player.position - transform.position).magnitude < Radius)
        {
            OutlineVisible = true;
        }
        else
        {
            OutlineVisible = false;
        }

        copy.SetActive(OutlineVisible); 
    }

    public void Interaction(string text)
    {
        Interacted = true;
        Player.GetComponent<PlayerActions_Level11>().TextBlack.GetComponent<TextMeshProUGUI>().text = text;
        Player.GetComponent<PlayerActions_Level11>().TextWhite.GetComponent<TextMeshProUGUI>().text = text;
        StartCoroutine(RemoveText());
    }

    IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(5);
        Player.GetComponent<PlayerActions_Level11>().TextBlack.GetComponent<TextMeshProUGUI>().text = "";
        Player.GetComponent<PlayerActions_Level11>().TextWhite.GetComponent<TextMeshProUGUI>().text = "";
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
#endif

}
