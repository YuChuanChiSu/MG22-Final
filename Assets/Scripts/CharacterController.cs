using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;
using GenericToolKit.Mvvm;
/// <summary>
/// ���������
/// </summary>
public class CharacterController : ObservableMonoBehavior
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
    [Tooltip("Ѫ��")]
    [SerializeField]
    private long _hp = 100;
    public long HP
    {
        get => _hp;
        set => SetProperty(ref _hp, value);
    }

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

    private void OnDestroy()
    {
        Dispose();
    }
}
