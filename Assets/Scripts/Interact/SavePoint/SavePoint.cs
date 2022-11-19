using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SavePoint : InteractBase
{
    /// <summary>
    /// 逻辑代码全在ViewModel里
    /// </summary>
    private SavePointViewModel _savePoint 
        => ServiceLocator.Instance?.SavePoint;

    /// <summary>
    /// 触碰到检查点后可能会有一些动画效果等等，留一个回调
    /// </summary>
    public static event UnityAction<SavePoint> OnSavePointTriggered;

    /// <summary>
    /// 是否为出生点（关卡初始位置，还未触发任何检查点时）
    /// </summary>
    public bool isBornPoint = false;

    private void Start()
    {
        if (isBornPoint)
        {
            Interact();
        }
    }

    public override bool Interact()
    {
        _savePoint.RecentSavePosition = CharacterController.Instance.GetComponent<Transform>().position;
        _savePoint.RecentHP = 40; // 固定恢复到40，忘了说明了qwq CharacterController.Instance.HP;
        OnSavePointTriggered?.Invoke(this);
        return true;
    }
}