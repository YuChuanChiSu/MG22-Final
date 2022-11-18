using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图可交互物体基础类
/// </summary>
public class InteractBase : MonoBehaviour
{
    /// <summary>
    /// 触发交互
    /// </summary>
    /// <returns>是否结束触发（以后不可触发）</returns>
    public virtual bool Interact()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// 是否可以交互（额外判定）
    /// </summary>
    /// <returns></returns>
    public virtual bool CanActive()
    {
        return true;
    }
}
