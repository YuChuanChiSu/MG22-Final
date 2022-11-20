using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlotSelector : PlotSelector 
{
    public GameObject HE, BE;
    public override string Select()
    {
        return "Pass";
    }
}
