using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotUnit : MonoBehaviour
{
    public string PlotTag;
    [TextArea]
    public string PlotCode;
    [Tooltip("��������Entry��ľ��������Ч��")]
    public bool ReInteractable = true;
}