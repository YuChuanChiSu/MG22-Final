using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÑªÌõ¿ØÖÆÆ÷
/// </summary>
public class HPController : MonoBehaviour
{
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
    }
}
