using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    CharacterController _chara;
    public HPController(CharacterController chara)
    {
        _chara = chara;
    }
}
