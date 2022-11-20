using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using static CharacterModel;

public class CharacterFormChanger : GenericToolKit.Mvvm.ObservableMonoBehavior
{
    CharacterController _chara;

    /// <summary>
    /// �Ƚ���
    /// </summary>
    public class CharacterFormEqualityComparer : IEqualityComparer<CharacterForm>
    {
        public bool Equals(CharacterForm x, CharacterForm y)
        {
            if (x == y)
                return true;

            if (CharacterController.Instance.HP - Damage <= 0)
            {
                OnHPNotEnough?.Invoke(y);
                return false;
            }

            if (CharacterFormLock.isLocked(y))
            {
                OnCharacterFormIsLocked?.Invoke(y);
                return false;
            }

            switch (y)
            {
                case CharacterForm.Ice:
                    return CharacterController.Instance.Temperature != Temperature.Zero;
                case CharacterForm.Mist:
                    return CharacterController.Instance.Temperature != Temperature.Boil;
                case CharacterForm.Water:
                    return CharacterController.Instance.Temperature != Temperature.Standard;
                default:
                    return false;
            }
        }

        public int GetHashCode(CharacterForm obj)
            => obj.GetHashCode();
    }

    private CharacterFormEqualityComparer comparer;

    /// <summary>
    /// �˺� //���߻������ַ�����...�˺�Ϊ0��
    /// </summary>
    public static long Damage;
    
    /// <summary>
    /// ��Ҫ�ı���̬��ֱ�Ӷ�������Ը�ֵ����
    /// </summary>
    public CharacterForm Form
    {
        get => _chara.Form;
        set
        {
            if (value == CharacterForm.Mist)
            {
                LevelPass.Instance.BgAnimator.SetFloat("Speed", 1.0f);
                LevelPass.Instance.BgAnimator.Play("BackgroundRotation", 0, 0.0f);
                LevelPass.Instance.ArrowAnimator.transform.localEulerAngles = new Vector3(0, 0, 180);
                LevelPass.Instance.ArrowAnimator.Play("ArrowAnimation", 0, 0.0f);
            }
            else if (Form == CharacterForm.Mist)
            {
                LevelPass.Instance.BgAnimator.SetFloat("Speed", -1.0f);
                LevelPass.Instance.BgAnimator.Play("BackgroundRotation", 0, 1.0f);
                LevelPass.Instance.ArrowAnimator.transform.localEulerAngles = new Vector3(0, 0, 0);
                LevelPass.Instance.ArrowAnimator.Play("ArrowAnimation", 0, 0.0f);
            }
            SetProperty(ref _chara.Form, value, comparer);
        }
    }

    /// <summary>
    /// ���ⲿ�ṩ�Ķ����ı�ص�������Ϊ������̬�ı�Ľ��
    /// </summary>
    public UnityAction<CharacterForm> OnAnimationChanging { get; set; }

    /// <summary>
    /// ���ⲿ�ṩ����ͼ�ı�ص�������Ϊ������̬�ı�Ľ��
    /// </summary>
    public UnityAction<CharacterForm> OnSpriteChanging { get; set; }

    /// <summary>
    /// ���ⲿ�ṩ��HP��������̬ת���Ķ����Ļص�������Ϊ����ϣ���ı����̬
    /// </summary>
    public static UnityAction<CharacterForm> OnHPNotEnough { get; set; }

    /// <summary>
    /// ���ⲿ�ṩ����̬δ�����Ķ����Ļص�������Ϊ����ϣ���ı����̬
    /// </summary>
    public static UnityAction<CharacterForm> OnCharacterFormIsLocked { get; set; }

    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
        comparer = new CharacterFormEqualityComparer();
    }

    private void OnCharacterFormChanged(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(Form))
        {
            OnAnimationChanging?.Invoke(Form);
            OnSpriteChanging?.Invoke(Form);
        }
    }

    private void OnDestroy()
    {
        Dispose();
    }
}
