using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public GameObject MiniMap;
    void Update()
    {
        bool touched = false;
        if (Input.GetMouseButton(0))
        {
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
            {
                if (hit.collider.gameObject.name == "FBack") touched = true;
            }
        }
        if (touched) return;
        MiniMap.SetActive((Input.GetKey(KeyCode.E) || InputController.IsPress[(int)KeyCode.E]) && !PlotController.PlotLock);
    }
}
