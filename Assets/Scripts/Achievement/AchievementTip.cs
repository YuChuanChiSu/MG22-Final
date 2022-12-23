using DG.Tweening;
using GenericToolKit.Mvvm.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementTip : ObservablePanelMono<AchievementTipVM>
{
    public override AchievementTipVM Panel
        => ServiceLocator.Instance.AchievementTipVM;



    private void Update()
    {
        switch (Globle.Death)
        {
            case 3:
                {
                    MoveTip(0);
                    break;
                }
            case 10:
                {
                    MoveTip(1);
                    break;
                }
            case 20:
                {
                    MoveTip(2);
                    break;
                }
            case 40:
                {
                    MoveTip(3);
                    break;
                }
            default:
                break;
        }

        if (Globle.GWaterDeath == 2)
            MoveTip(4);

        if (Globle.GFireDeath == 5)
            MoveTip(5);

        if (Globle.GOutDeath2 == 5)
            MoveTip(6);

        if (Globle.GIceDeath == 5)
            MoveTip(7);

        switch (Globle.GSwitch)
        {
            case 5:
                {
                    MoveTip(8);
                    break;
                }
            case 10:
                {
                    MoveTip(9);
                    break;
                }
            case 20:
                {
                    MoveTip(10);
                    break;
                }
            case 50:
                {
                    MoveTip(11);
                    break;
                }
            default:
                break;
        }


    }

    private void MoveTip(int index)
    {
        Panel.ImageMovement(index);
    }
}