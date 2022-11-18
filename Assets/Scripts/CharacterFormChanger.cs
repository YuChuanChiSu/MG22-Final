using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFormChanger : MonoBehaviour
{
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
    }
}
