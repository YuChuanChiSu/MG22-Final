using GenericToolKit.Mvvm;
using GenericToolKit.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HidePanelViewModel : ObservablePanel
{
    public Text GetHPEmptyText()
        => GetCompoent<Text>("HPEmptyText");

    public Image GetPanelImage()
        => GetCompoent<Image>("HidePanel");
}