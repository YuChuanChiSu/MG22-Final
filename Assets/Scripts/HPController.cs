using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ѫ��������
/// </summary>
public class HPController : MonoBehaviour
{
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
    }
}
