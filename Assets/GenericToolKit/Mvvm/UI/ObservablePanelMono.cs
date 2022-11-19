using GenericToolKit.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GenericToolKit.Mvvm.UI
{
    public abstract class ObservablePanelMono<TObservablePanel> : MonoBehaviour where TObservablePanel : ObservablePanel
    {
        public abstract TObservablePanel Panel { get; }

        protected virtual void Awake()
        {
            Panel.GetPanelUICompoents(gameObject);
        }
        
        protected virtual void OnDestroy()
        {
            Panel?.Dispose();
        }
    }
}
