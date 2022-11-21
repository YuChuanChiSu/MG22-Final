using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SpringWater : InteractBase
{
    /// <summary>
    /// 触碰到泉水后可能会有一些动画效果等等，留一个回调
    /// </summary>
    public static event UnityAction<SpringWater> OnSpringWaterTriggered;

    public override bool Interact()
    {
        CharacterController.Instance.HP = 80;
        OnSpringWaterTriggered?.Invoke(this);
        return false;
    }
}