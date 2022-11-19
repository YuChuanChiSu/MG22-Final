using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotLevelPass : PlotSelector
{
    public override string Select()
    {
        if (LevelPass.Step >= LevelPass.Instance.MaxStep - 1)
        {
            return "Pass";
        }
        else
        {
            return "Fail";
        }
    }
}
