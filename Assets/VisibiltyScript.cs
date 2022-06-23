using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibiltyScript : MonoBehaviour
{
    public GameObject GuardCollider;
   public void DoorOpen()
    {
        GuardCollider.SetActive(true);
    }
    public void DoorClosed()
    {
        GuardCollider.SetActive(false);
    }
}
