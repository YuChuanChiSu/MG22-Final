using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ﶯ��������
/// </summary>
public class CharacterAnimator : MonoBehaviour
{
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
    }
}
