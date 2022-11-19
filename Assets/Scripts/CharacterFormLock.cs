using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;

public class CharacterFormLock
{
    private static Dictionary<CharacterForm, bool> locks;
    static CharacterFormLock()
    {
        locks = new Dictionary<CharacterForm, bool>();
        locks.Add(CharacterForm.Ice, true);
        locks.Add(CharacterForm.Water, false);
        locks.Add(CharacterForm.Mist, true);
    }
    /// <summary>
    /// 是否还未解锁某种形态
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public static bool isLocked(CharacterForm form)
    {
        return locks[form];
    }
    /// <summary>
    /// 解锁某种形态
    /// </summary>
    /// <param name="form"></param>
    public static void UnLock(CharacterForm form)
    {
        locks[form] = false;
    }
}
