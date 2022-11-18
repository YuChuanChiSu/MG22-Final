using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GenericToolKit.Mvvm.UI
{
    #nullable enable
    public abstract class ObservablePanel: ObservableObject, INotifyButtonClicked, INotifyToggleValueChanged
    {
        private IDictionary<string, IList<UIBehaviour>> _compents
            = new Dictionary<string, IList<UIBehaviour>>();

        public event ButtonClickedEventHandler? ButtonClicked;
        public event ToggleValueChangedEventHandler? ToggleValueChanged;

        public virtual void GetPanelUICompoents(GameObject panel)
        {
            GetUICompoentsInChildren(panel.GetComponentsInChildren<Button>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<Image>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<Text>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<TMP_Text>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<Toggle>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<Slider>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<ScrollRect>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<InputField>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<TMP_InputField>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<Dropdown>());
            GetUICompoentsInChildren(panel.GetComponentsInChildren<RawImage>());
        }

        protected TUIBehaviour GetCompoent<TUIBehaviour>(string compoentName) where TUIBehaviour : UIBehaviour
        {
            if (_compents.ContainsKey(compoentName))
            {
                UIBehaviour compoent = _compents[compoentName].Where(behaviour => behaviour is TUIBehaviour).FirstOrDefault();
                if (compoent is not default(UIBehaviour))
                    return (TUIBehaviour)compoent;
            }

            throw new ArgumentException($"Not find compoent in cache {typeof(TUIBehaviour).Name}: {compoentName}.");
        }

        private void GetUICompoentsInChildren<TUIBehaviour>(TUIBehaviour[] behaviours) where TUIBehaviour : UIBehaviour
        {
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (!_compents.ContainsKey(behaviours[i].name))
                    _compents.Add(behaviours[i].name, new List<UIBehaviour>());

                _compents[behaviours[i].name].Add(behaviours[i]);
                ResolveComponent(behaviours[i]);
            }
        }

        protected virtual void ResolveComponent<TUIBehaviour>(TUIBehaviour compoent) where TUIBehaviour : UIBehaviour
        {
            if (compoent is Button)
                (compoent as Button)?.onClick.AddListener(() => ButtonClicked?.Invoke(compoent.name));

            else if (compoent is Toggle)
                (compoent as Toggle)?.onValueChanged.AddListener((value) => ToggleValueChanged?.Invoke(value, compoent.name));
        }

        public override void Dispose()
        {
            base.Dispose();
            _compents.Clear();
            ButtonClicked = null;
        }
    }
}
