using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlotLock : MonoBehaviour
{
    public GameObject BGM;
    public void PutLock()
    {
        PlotController.PlotLock = true;
        //BGM.SetActive(false);
    }
    public void UnLock()
    {
        PlotController.PlotLock = false;
        //BGM.SetActive(true);
    }
}
