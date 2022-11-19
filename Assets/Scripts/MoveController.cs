using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÒÆ¶¯¿ØÖÆÆ÷
/// </summary>
public class MoveController : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    Transform _camera;
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = GameObject.Find("CM vcam1").transform;
    }
    public void Update()
    {
        Vector3 rotation = new Vector3(0, 0, _chara.isHandstand ? 180 : 0);
        _camera.localEulerAngles += (rotation - _camera.localEulerAngles) / 30;
        _rigidbody.gravityScale = 10 * (_chara.isHandstand ? -1.0f : 1.0f);
        Vector2 v = _rigidbody.velocity;
        if (!PlotController.PlotLock)
        {
            if (Input.GetKey(KeyCode.D))
            {
                _chara.spriteRenderer.flipX = !_chara.isHandstand;
                v.x = _chara.MoveSpeed * (_chara.isHandstand ? -1.0f : 1.0f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _chara.spriteRenderer.flipX = _chara.isHandstand;
                v.x = (-1) * _chara.MoveSpeed * (_chara.isHandstand ? -1.0f : 1.0f);
            }
            else
            {
                v.x = 0;
            }
            if (Input.GetKey(KeyCode.Space) && (_chara.State == CharacterModel.CharacterState.Still || _chara.State == CharacterModel.CharacterState.Walk))
            {
                v.y = _chara.JumpDegree * (_chara.isHandstand ? -1.0f : 1.0f);
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                _chara.isHandstand = !_chara.isHandstand;
            }
        }
        _rigidbody.velocity = v;
        if (v.y == 0 && _chara.State == CharacterModel.CharacterState.Jump)
            _chara.State = CharacterModel.CharacterState.Still;
        if (_chara.State == CharacterModel.CharacterState.Still && v.x != 0)
            _chara.State = CharacterModel.CharacterState.Walk;
        if (v.y != 0)
            _chara.State = CharacterModel.CharacterState.Jump;
        if (v.x == 0 && v.y == 0)
            _chara.State = CharacterModel.CharacterState.Still;
    }
}
