using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 情节选择器（入口点模式，即不做选择直接执行Entry对应的情节单元）
/// </summary>
public class PlotEntry : PlotSelector
{
    public override string Select()
    {
        return "Entry";
    }
}
