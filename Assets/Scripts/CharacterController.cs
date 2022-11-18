using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;
/// <summary>
/// 人物控制器
/// </summary>
public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// 当前人物实例
    /// </summary>
    public static CharacterController Instance { get; private set; }

    /// <summary>
    /// 人物形态
    /// </summary>
    public CharacterForm Form = CharacterForm.Water;
    /// <summary>
    /// 人物状态
    /// </summary>
    public CharacterState State = CharacterState.Still;
    /// <summary>
    /// 温度
    /// </summary>
    public Temperature Temperature = Temperature.Standard;
    /// <summary>
    /// 是否处于倒立状态
    /// </summary>
    public bool isHandstand = false;
    [Tooltip("移动速度")]
    public float MoveSpeed = 5;
    [Tooltip("跳跃幅度")]
    public float JumpDegree = 25;
    /// <summary>
    /// 血量
    /// </summary>
    public long HP = 100;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public CharacterAnimator characterAnimator;
    [HideInInspector]
    public CharacterFormChanger characterFormChanger;
    [HideInInspector]
    public MoveController moveController;
    [HideInInspector]
    public HPController hpController;

    private void Awake()
    {
        characterAnimator = gameObject.AddComponent<CharacterAnimator>();
        characterFormChanger = gameObject.AddComponent<CharacterFormChanger>();
        moveController = gameObject.AddComponent<MoveController>();
        hpController = gameObject.AddComponent<HPController>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        Instance = this;
    }
}
