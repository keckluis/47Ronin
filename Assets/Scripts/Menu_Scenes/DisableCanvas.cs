using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvas : MonoBehaviour
{
    public int seconds = 15;
    GameObject Image;
    bool move = false;
    void Awake() 
    {
        Image = GameObject.Find("HintImage");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disable());
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
            Image.transform.Translate(Vector3.down * Time.deltaTime * 600, Space.World);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(seconds);
        move = true;    
        yield return new WaitForSeconds(5);
        GameObject.Destroy(this);
    }
}
