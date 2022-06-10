using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform Player;
    public int OffsetX = -10;
    public int OffsetY = 5;
    public int OffsetZ = -15;

    void Update()
    {
        float x = Player.position.x + OffsetX;
        float y = Player.position.y + OffsetY;
        float z = Player.position.z + OffsetZ;
        transform.position = new Vector3(x, y, z);
    }
}
