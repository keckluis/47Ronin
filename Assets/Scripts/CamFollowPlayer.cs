using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public Transform Player;
    public int OffsetX;
    public int OffsetY;
    public int OffsetZ;

    void Update()
    {
        transform.position = new Vector3(Player.position.x + OffsetX, Player.position.y + OffsetY, OffsetZ);
    }
}
