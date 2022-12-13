using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera Camera;
    public GameObject Controller;

    void Update()
    {
        float size = (PlotController.PlotLock ? 3.0f : 7.0f);
        Camera.m_Lens.OrthographicSize += (size - Camera.m_Lens.OrthographicSize) / 40f;
        Controller.SetActive(!PlotController.PlotLock);
    }
}
