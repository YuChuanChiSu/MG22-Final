using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBinder : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
