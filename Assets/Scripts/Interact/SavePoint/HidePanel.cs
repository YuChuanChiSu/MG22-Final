using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericToolKit.Mvvm.UI;

public class HidePanel : ObservablePanelMono<HidePanelViewModel>
{
    public override HidePanelViewModel Panel
        => ServiceLocator.Instance?.HidePanel;

    protected override void Awake()
    {
        base.Awake();
    }


}