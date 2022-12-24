using GenericToolKit.Mvvm.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanel : ObservablePanelMono<AchievementCollection>
{
    public override AchievementCollection Panel
        => ServiceLocator.Instance.AchievementCollection;

    private void OnEnable()
    {
        int i = 0;
        foreach(var item in Panel.Achievements)
        {
            if (i == 12)
                break;
            Panel.SetPanelEnable(i++, item);
        }
    }

}