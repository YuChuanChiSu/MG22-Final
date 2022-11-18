using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;

public class TemperatureController : MonoBehaviour
{
    CharacterController _chara;
    public TemperatureController(CharacterController chara)
    {
        _chara = chara;
    }
}
