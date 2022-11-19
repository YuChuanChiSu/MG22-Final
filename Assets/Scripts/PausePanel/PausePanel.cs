using GenericToolKit.Mvvm;
using GenericToolKit.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PausePanel : ObservablePanelMono<PausePanelViewModel>
{
    public override PausePanelViewModel Panel
        => ServiceLocator.Instance?.PausePanel;

    protected override void Awake()
    {
        base.Awake();
        Panel.SetActive();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Panel.OnButtonClick("PauseButton");
    }

    public void OnClick(string btnName)
    {
        Panel.OnButtonClick(btnName);
    }
}