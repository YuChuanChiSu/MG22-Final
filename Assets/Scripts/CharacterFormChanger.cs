using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using static CharacterModel;

public class CharacterFormChanger : ObservableMonoBehavior
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
    /// �˺�
    /// </summary>
    public long Damage;
    
    /// <summary>
    /// ��Ҫ�ı���̬��ֱ�Ӷ�������Ը�ֵ����
    /// </summary>
    public CharacterForm Form
    {
        get => _chara.Form;
        set => SetProperty(ref _chara.Form, value, comparer);
    }

    /// <summary>
    /// ���ṩ�ⲿ�Ķ����ı�ί�У�����Ϊ������̬�ı�Ľ��
    /// </summary>
    public UnityAction<CharacterForm> OnAnimationChanging { get; set; }

    /// <summary>
    /// ���ṩ�ⲿ����ͼ�ı�ί�У�����Ϊ������̬�ı�Ľ��
    /// </summary>
    public UnityAction<CharacterForm> OnSpriteChanging { get; set; }

    /// <summary>
    /// ���ṩ�ⲿ��HP����ί�У�����Ϊ������̬�任���µ����һ���˺�
    /// </summary>
    public UnityAction<long> OnHPEmpty { get; set; }

    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
        comparer = new CharacterFormEqualityComparer();
    }

    private void OnCharacterFormChanged(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(Form))
        {
            OnAnimationChanging(Form);
            OnSpriteChanging(Form);

            if (_chara.HP - Damage <= 0)
                OnHPEmpty(Damage);
        }
    }


    private void OnDestroy()
    {
        Dispose();
    }
}
