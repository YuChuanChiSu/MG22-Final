using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlotSelector : PlotSelector
{
    public Sprite WaterSprite;
    public override string Select()
    {
        if (CharacterController.Instance.Form == CharacterModel.CharacterForm.Ice)
        {
            return "Fail";
        }
        else
        {
            return "Entry";
        }
    }
}
