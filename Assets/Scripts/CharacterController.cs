using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;
/// <summary>
/// ���������
/// </summary>
public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// ��ǰ����ʵ��
    /// </summary>
    public static CharacterController Instance { get; private set; }

    /// <summary>
    /// ������̬
    /// </summary>
    public CharacterForm Form = CharacterForm.Water;
    /// <summary>
    /// ����״̬
    /// </summary>
    public CharacterState State = CharacterState.Still;
    /// <summary>
    /// �¶�
    /// </summary>
    public Temperature Temperature = Temperature.Standard;
    /// <summary>
    /// �Ƿ��ڵ���״̬
    /// </summary>
    public bool isHandstand = false;
    [Tooltip("�ƶ��ٶ�")]
    public float MoveSpeed = 5;
    [Tooltip("��Ծ����")]
    public float JumpDegree = 25;
    /// <summary>
    /// Ѫ��
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
