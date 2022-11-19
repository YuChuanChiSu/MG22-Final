using GenericToolKit.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelViewModel : ObservablePanel
{
    public PausePanelViewModel()
    {
        ButtonClicked += OnButtonClick;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Pause()
    {
        PlotController.PlotLock = true;
        Time.timeScale = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Restart()
    {
        Time.timeScale = 1;
        PlotController.PlotLock = false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Exit()
        => UnityEngine.Application.Quit();

    public void SetActive()
        => SetCompoent(true);

    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "PauseButton":
                Pause();
                SetCompoent(false);
                break;
            case "ContinueButton":
                Restart();
                SetCompoent(true);                
                break;
            case "ExitButton":
                Exit();
                break;
            default:
                break;
        }
    }

    private void SetCompoent(bool active)
    {
        GetCompoent<Button>("PauseButton").gameObject.SetActive(active);
        GetCompoent<Image>("PauseContainImage").gameObject.SetActive(!active);
    }

}