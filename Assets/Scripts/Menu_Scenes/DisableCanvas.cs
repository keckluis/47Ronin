using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvas : MonoBehaviour
{
    public int seconds = 10;
    public bool top = false;
    GameObject Image;
    bool move = false;
    Vector3 dir = Vector3.down;

    void Awake() 
    {
        Image = GameObject.Find("HintImage");        
        if (top) {
            RectTransform pos = Image.transform.GetComponent<RectTransform>();
            pos.transform.position = new Vector3(pos.transform.position.x, 1025, pos.transform.position.z);
            pos.transform.localRotation = Quaternion.Euler(new Vector3(180, 0, 0));
            GameObject text = GameObject.Find("HintText (TMP)");
            text.transform.localScale = new Vector3(1, -1, 1);
            text.transform.localPosition = new Vector3(0, 100, 0);
            dir = Vector3.up;
        }
}

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disable());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move){
            Image.transform.Translate(dir * Time.deltaTime * 600, Space.World);
        }    
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(seconds);
        move = true;    
        yield return new WaitForSeconds(5);
        this.enabled = false;
    }
}
