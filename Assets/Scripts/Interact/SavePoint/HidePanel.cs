using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericToolKit.Mvvm.UI;

public class HidePanel : ObservablePanelMono<HidePanelViewModel>
{
    public override HidePanelViewModel Panel { get; set; }

    protected override void Awake()
    {
        Panel = ServiceLocator.Instance.HidePanel;
        base.Awake();
    }


}