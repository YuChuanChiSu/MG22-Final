using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    CharacterController _chara;
    public CharacterAnimator(CharacterController chara)
    {
        _chara = chara;
    }
}
