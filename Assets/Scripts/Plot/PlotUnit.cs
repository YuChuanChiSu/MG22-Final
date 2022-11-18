using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotUnit : MonoBehaviour
{
    public string PlotTag;
    [TextArea]
    public string PlotCode;
    [Tooltip("该设置在Entry外的剧情组件无效。")]
    public bool ReInteractable = true;
}