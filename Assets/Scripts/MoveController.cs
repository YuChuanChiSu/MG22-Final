using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÒÆ¶¯¿ØÖÆÆ÷
/// </summary>
public class MoveController : MonoBehaviour
{
    public const int MaxJumpCount = 1;
    public static int JumpCount = 0;
    public Rigidbody2D _rigidbody;
    Transform _camera;
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = GameObject.Find("CM vcam1").transform;
    }
    private void FixedUpdate()
    {
        Vector2 v = _rigidbody.velocity;
        if (Math.Abs(v.y) < 0.1f && _chara.State == CharacterModel.CharacterState.Jump)
        {
            _chara.State = CharacterModel.CharacterState.Still;
        }
        if (_chara.State == CharacterModel.CharacterState.Still && v.x != 0)
            _chara.State = CharacterModel.CharacterState.Walk;
        if (Math.Abs(v.y) >= 0.1f)
            _chara.State = CharacterModel.CharacterState.Jump;
        if (v.x == 0 && v.y == 0)
        {
            _chara.State = CharacterModel.CharacterState.Still;
        }
    }
    public void Update()
    {
        Vector3 rotation = new Vector3(0, 0, _chara.isHandstand ? 180 : 0);
        _camera.localEulerAngles += (rotation - _camera.localEulerAngles) / 30;
        Vector2 v = _rigidbody.velocity;
        bool falling = (!_chara.isHandstand && v.y <= 0) || (_chara.isHandstand && v.y >= 0);
        _rigidbody.gravityScale = 10 * (_chara.isHandstand ? -1.0f : 1.0f) * (falling ? 0.3f : 1.0f);
        if (!PlotController.PlotLock)
        {
            if (!(JumpCount > 0 && Math.Abs(v.y) <= 0.1f))
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
            }
            else
            {
                v.x = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space) && JumpCount < MaxJumpCount && falling && _chara.Form != CharacterModel.CharacterForm.Water)
            {
                v.y = _chara.JumpDegree * (_chara.isHandstand ? -1.0f : 1.0f);
                JumpCount++;
                //Debug.Log(JumpCount + "¶ÎÌø£¡");
            }

        }
        _chara.isHandstand = _chara.Form == CharacterModel.CharacterForm.Mist;
        _rigidbody.velocity = v;
    }
}
