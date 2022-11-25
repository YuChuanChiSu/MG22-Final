using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public GameObject MiniMap;
    void Update()
    {
        MiniMap.SetActive(Input.GetKey(KeyCode.E) && !PlotController.PlotLock);
    }
}
