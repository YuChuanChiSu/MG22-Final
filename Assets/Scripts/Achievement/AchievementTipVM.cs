using GenericToolKit.Mvvm.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG;
using System.Reflection;

public class AchievementTipVM : ObservablePanel
{
    public void ImageMovement(int index)
    {
        if (ServiceLocator.Instance.AchievementCollection.IsAwarded((AchievementEnum)index))
            return;

        Image image = GetCompoent<Image>($"Achievement{index}");
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1f);
        sequence.Append(image.rectTransform.DOAnchorPosX(-160, 4f).OnComplete(()
            =>
        {
            image.rectTransform.DOAnchorPosX(340, 4f);
        }));

        ServiceLocator.Instance.AchievementCollection.MakeAwarded((AchievementEnum)index);
    }
}