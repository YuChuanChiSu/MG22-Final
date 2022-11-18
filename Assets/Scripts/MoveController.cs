using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    CharacterController _chara;
    public MoveController(CharacterController chara)
    {
        _chara = chara;
    }
}
