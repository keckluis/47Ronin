using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditLinks : MonoBehaviour
{
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
