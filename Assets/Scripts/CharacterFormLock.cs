using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;

/// <summary>
/// ���Źؿ��ƽ���������̬�𲽽�������ζ��ĳЩ��̬�������ǿ��ܵģ�
/// </summary>
public class CharacterFormLock
{
    private static bool[] locks;
    static CharacterFormLock()
    {
        locks = new bool[] { true, false, true };
    }
    /// <summary>
    /// �ж�ָ����̬�Ƿ񻹲���ʹ��
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public static bool isLocked(CharacterForm form)
    {
        return locks[(int)form];
    }
    /// <summary>
    /// ����ָ��״̬
    /// </summary>
    /// <param name="form"></param>
    public static void UnLock(CharacterForm form)
    {
        locks[(int)form] = false;
    }
}
