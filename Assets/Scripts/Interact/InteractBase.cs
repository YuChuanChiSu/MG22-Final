using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ�ɽ������������
/// </summary>
public class InteractBase : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    /// <returns>�Ƿ�����������Ժ󲻿ɴ�����</returns>
    public virtual bool Interact()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// �Ƿ���Խ����������ж���
    /// </summary>
    /// <returns></returns>
    public virtual bool CanActive()
    {
        return true;
    }
}
