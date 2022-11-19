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

    public override bool Interact()
    {
        _savePoint.RecentSavePosition = CharacterController.Instance.GetComponent<Transform>().position;
        _savePoint.RecentHP = CharacterController.Instance.HP;
        OnSavePointTriggered?.Invoke(this);
        return true;
    }
}