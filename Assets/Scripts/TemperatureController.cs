using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;

/// <summary>
/// ÎÂ¶È¿ØÖÆÆ÷
/// </summary>
public class TemperatureController : MonoBehaviour
{
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
    }
}
