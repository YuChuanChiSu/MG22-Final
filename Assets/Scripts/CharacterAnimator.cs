using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人物动画播放器
/// </summary>
public class CharacterAnimator : MonoBehaviour
{
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
    }
}
