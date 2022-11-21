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

    private static long LastHP = 80;
    private const int _timeControl = 50;
    private const float _adder = 0.02f; 
    private Text _hpEmptyText;
    private Image _hidePanel;

    public SavePointViewModel(ISyncDispatcher dispatcher)
    {
        CharacterController.Instance.PropertyChanged += OnHPChanged;
        _dispatcher = dispatcher;
        _hpEmptyText = ServiceLocator.Instance?.HidePanel.GetHPEmptyText();
        _hidePanel = ServiceLocator.Instance?.HidePanel.GetPanelImage();
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
            GameObject HPTip = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\HPTip"), 
                                                        CharacterController.Instance.transform.position, 
                                                        CharacterController.Instance.transform.localRotation, 
                                                        GameObject.Find("TipCanvas").transform);
            Text HPText = HPTip.GetComponent<Text>();
            HPText.text = (LastHP < CharacterController.Instance.HP ? "+" : "") + (CharacterController.Instance.HP - LastHP);
            HPText.color = (LastHP < CharacterController.Instance.HP ? Color.green : Color.red);
            Debug.Log(CharacterController.Instance.isHandstand);
            LastHP = CharacterController.Instance.HP;
            HPTip.SetActive(true);
            HPTip.transform.localEulerAngles = new Vector3(0, 0, CharacterController.Instance.isHandstand ? 180 : 0);
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
        PlotController.PlotLock = true;

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
        CharacterController.Instance.moveController._rigidbody.velocity = Vector3.zero;
        CharacterController.Instance.isHandstand = false;
        TemperatureController.Instance.TemperatureReset();
        CharacterController.Instance.characterFormChanger.Form = CharacterModel.CharacterForm.Water;

        for (int i = 0; i < _timeControl; i++)
            yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < _timeControl; i++)
        {
            _hidePanel.color = new Color(_hidePanel.color.r, _hidePanel.color.g, _hidePanel.color.b, _hidePanel.color.a - _adder);
            _hpEmptyText.color = new Color(_hpEmptyText.color.r, _hpEmptyText.color.g, _hpEmptyText.color.b, _hpEmptyText.color.a - _adder);
            yield return new WaitForSeconds(0.01f);
        }

        PlotController.PlotLock = false;
    }

    public override void Dispose()
    {
        base.Dispose();
        //CharacterController.Instance.PropertyChanged -= OnHPChanged;
    }

    public void Goodbye()
    {
        CharacterController.Instance.PropertyChanged -= OnHPChanged;
    }

    public void UpdateRua()
    {
        CharacterController.Instance.PropertyChanged += OnHPChanged;
        _hpEmptyText = ServiceLocator.Instance?.HidePanel.GetHPEmptyText();
        _hidePanel = ServiceLocator.Instance?.HidePanel.GetPanelImage();
    }
}