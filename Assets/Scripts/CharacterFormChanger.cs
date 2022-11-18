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
    /// 比较器
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
    /// 伤害
    /// </summary>
    public long Damage;
    
    /// <summary>
    /// 若要改变形态，直接对这个属性赋值即可
    /// </summary>
    public CharacterForm Form
    {
        get => _chara.Form;
        set => SetProperty(ref _chara.Form, value, comparer);
    }

    /// <summary>
    /// 由提供外部的动画改变委托，参数为本次形态改变的结果
    /// </summary>
    public UnityAction<CharacterForm> OnAnimationChanging { get; set; }

    /// <summary>
    /// 由提供外部的贴图改变委托，参数为本次形态改变的结果
    /// </summary>
    public UnityAction<CharacterForm> OnSpriteChanging { get; set; }

    /// <summary>
    /// 由提供外部的HP归零委托，参数为本次形态变换导致的最后一次伤害
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
