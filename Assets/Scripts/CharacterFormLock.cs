using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;

/// <summary>
/// 随着关卡推进，部分形态逐步解锁（意味着某些形态不可用是可能的）
/// </summary>
public class CharacterFormLock
{
    private static bool[] locks;
    static CharacterFormLock()
    {
        locks = new bool[] { true, false, true };
    }
    /// <summary>
    /// 判断指定形态是否还不能使用
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public static bool isLocked(CharacterForm form)
    {
        return locks[(int)form];
    }
    /// <summary>
    /// 解锁指定状态
    /// </summary>
    /// <param name="form"></param>
    public static void UnLock(CharacterForm form)
    {
        locks[(int)form] = false;
    }
}
