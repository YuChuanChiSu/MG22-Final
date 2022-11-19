using GenericToolKit.Mvvm;
using GenericToolKit.Mvvm.Async;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SavePointViewModel : ObservableObject
{
    /// <summary>
    /// 异步容器
    /// </summary>
    private ISyncDispatcher _dispatcher;

    /// <summary>
    /// 最近一次的存档位置
    /// </summary>
    public Vector3 RecentSavePosition { get; set; }

    /// <summary>
    /// 最近一次的HP值
    /// </summary>
    public long RecentHP { get; set; }

    public UnityAction MovePause { get; set; }
    public UnityAction MoveRestart { get; set; }

    private const int _timeControl = 50;
    private const float _adder = 0.02f; 
    private Text _hpEmptyText;
    private Image _hidePanel;

    public SavePointViewModel(ISyncDispatcher dispatcher)
    {
        CharacterController.Instance.PropertyChanged += OnHPChanged;
        _dispatcher = dispatcher;
        _hpEmptyText = ServiceLocator.Instance.HidePanel.GetHPEmptyText();
        _hidePanel = ServiceLocator.Instance.HidePanel.GetPanelImage();
    }

    /// <summary>
    /// HP变化事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void OnHPChanged(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == "HP")
        {
            if (CharacterController.Instance.HP <= 0)
            {
                _dispatcher.StartCoroutine(ResetCorotine());
            }
        }
    }

    /// <summary>
    /// 黑幕动画，最好能再加入一点HP检测
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetCorotine()
    {
        MovePause?.Invoke();

        for (int i = 0; i < _timeControl; i++)
        {
            _hidePanel.color = new Color(_hidePanel.color.r, _hidePanel.color.g, _hidePanel.color.b, _hidePanel.color.a + _adder);
            _hpEmptyText.color = new Color(_hpEmptyText.color.r, _hpEmptyText.color.g, _hpEmptyText.color.b, _hpEmptyText.color.a + _adder);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < _timeControl; i++)
            yield return new WaitForSeconds(0.01f);

        CharacterController.Instance.gameObject.transform.position = RecentSavePosition;
        CharacterController.Instance.HP = RecentHP;
        // 重置其他状态
        CharacterController.Instance.Form = CharacterModel.CharacterForm.Water;
        CharacterController.Instance.isHandstand = false;

        for (int i = 0; i < _timeControl; i++)
            yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < _timeControl; i++)
        {
            _hidePanel.color = new Color(_hidePanel.color.r, _hidePanel.color.g, _hidePanel.color.b, _hidePanel.color.a - _adder);
            _hpEmptyText.color = new Color(_hpEmptyText.color.r, _hpEmptyText.color.g, _hpEmptyText.color.b, _hpEmptyText.color.a - _adder);
            yield return new WaitForSeconds(0.01f);
        }

        MoveRestart?.Invoke();
    }

    public override void Dispose()
    {
        base.Dispose();
        CharacterController.Instance.PropertyChanged -= OnHPChanged;
    }
}