using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryHandler : MonoBehaviour
{
    public Transform Camera;
    public float Y = 0;
    public float Z = 0;

    public List<float> Positions;

    private int currentPos = 0;
    private bool isMoving = false;
    static float t = 0.0f;

    private void Update()
    {
        if (isMoving)
        {
            Camera.localPosition = new Vector3(Mathf.Lerp(Camera.localPosition.x, Positions[currentPos], t), Y, Z);
            t += 0.5f * Time.deltaTime;

            if (t > 1.0f)
            {
                t = 0.0f;
                isMoving = false;
            }
        }
    }

    public void Next()
    {
        if (currentPos + 1 < Positions.Count && !isMoving)
        {
            isMoving = true;
            currentPos += 1;
        }    
    }

    public void Previous()
    {
        if (currentPos > 0 && !isMoving)
        {
            isMoving = true;
            currentPos -= 1;
        }      
    }
}
