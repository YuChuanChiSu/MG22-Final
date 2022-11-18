using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericToolKit.Mvvm.UI
{
    public delegate void ToggleValueChangedEventHandler(bool newValue, string toggleName);
    #nullable enable
    public interface INotifyToggleValueChanged
    {
        event ToggleValueChangedEventHandler? ToggleValueChanged;
    }
}
