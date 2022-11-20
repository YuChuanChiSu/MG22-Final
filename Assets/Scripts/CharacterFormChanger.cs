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
    /// 伤害 //（策划后来又废弃了...伤害为0）
    /// </summary>
    public static long Damage;
    
    /// <summary>
    /// 若要改变形态，直接对这个属性赋值即可
    /// </summary>
    public CharacterForm Form
    {
        get => _chara.Form;
        set
        {
            if (value == CharacterForm.Mist)
            {
                SndPlayer.Play("屏幕旋转");
                LevelPass.Instance.BgAnimator.SetFloat("Speed", 1.0f);
                LevelPass.Instance.BgAnimator.Play("BackgroundRotation", 0, 0.0f);
                LevelPass.Instance.ArrowAnimator.transform.localEulerAngles = new Vector3(0, 0, 180);
                LevelPass.Instance.ArrowAnimator.Play("ArrowAnimation", 0, 0.0f);
            }
            else if (Form == CharacterForm.Mist)
            {
                SndPlayer.Play("屏幕旋转");
                LevelPass.Instance.BgAnimator.SetFloat("Speed", -1.0f);
                LevelPass.Instance.BgAnimator.Play("BackgroundRotation", 0, 1.0f);
                LevelPass.Instance.ArrowAnimator.transform.localEulerAngles = new Vector3(0, 0, 0);
                LevelPass.Instance.ArrowAnimator.Play("ArrowAnimation", 0, 0.0f);
            }
            SetProperty(ref _chara.Form, value, comparer);
        }
    }

    /// <summary>
    /// 由外部提供的动画改变回调，参数为本次形态改变的结果
    /// </summary>
    public UnityAction<CharacterForm> OnAnimationChanging { get; set; }

    /// <summary>
    /// 由外部提供的贴图改变回调，参数为本次形态改变的结果
    /// </summary>
    public UnityAction<CharacterForm> OnSpriteChanging { get; set; }

    /// <summary>
    /// 由外部提供的HP不足以形态转换的动画的回调，参数为本次希望改变的形态
    /// </summary>
    public static UnityAction<CharacterForm> OnHPNotEnough { get; set; }

    /// <summary>
    /// 由外部提供的形态未解锁的动画的回调，参数为本次希望改变的形态
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
