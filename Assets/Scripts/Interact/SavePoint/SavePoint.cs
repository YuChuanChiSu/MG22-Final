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
    private SavePointViewModel _savePoint { get; set; }

    /// <summary>
    /// 触碰到检查点后可能会有一些动画效果等等，留一个回调
    /// </summary>
    public static event UnityAction<SavePoint> OnSavePointTriggered;
    
    /// <summary>
    /// 由外部提供移动暂停方法
    /// </summary>
    public UnityAction MovePause { get; set; }

    /// <summary>
    /// 由外部提供移动恢复方法
    /// </summary>
    public UnityAction MoveRestart { get; set; }

    /// <summary>
    /// 是否为出生点（关卡初始位置，还未触发任何检查点时）
    /// </summary>
    public bool isBornPoint = false;

    private void Start()
    {
        _savePoint = ServiceLocator.Instance.SavePoint;
        _savePoint.MovePause = MovePause;
        _savePoint.MoveRestart = MoveRestart;
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