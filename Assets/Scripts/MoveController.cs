using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        Vector2 v = _rigidbody.velocity;
        if (Input.GetKey(KeyCode.D))
        {
            v.x = _chara.MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            v.x = (-1) * _chara.MoveSpeed;
        }
        else
        {
            v.x = 0;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            v.y = -5;
        }
        _rigidbody.velocity = v;
        if (_chara.State == CharacterModel.CharacterState.Still && v.x != 0)
            _chara.State = CharacterModel.CharacterState.Walk;
        if (v.y != 0)
            _chara.State = CharacterModel.CharacterState.Jump;
    }
}
