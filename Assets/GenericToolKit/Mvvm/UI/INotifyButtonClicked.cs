using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericToolKit.Mvvm.UI
{
    public delegate void ButtonClickedEventHandler(string buttonName);
    #nullable enable
    public interface INotifyButtonClicked
    {
        event ButtonClickedEventHandler? ButtonClicked;
    }
}
