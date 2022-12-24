using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectBtn : MonoBehaviour
{
    public static LevelSelectBtn Active;
    public Sprite Deactive, Active1, Active2, Background, Light;
    public string TargetScene;
    public Image BgUI, BtnUI, BtnTextUI, LightUI;
    public bool isActive = false;
    private void Awake()
    {
        UpdateApppearance();
    }
    public void UpdateApppearance()
    {
        if (isActive)
        {
            if (Active != null)
            {
                Active.UpdateApppearance();
            }
            Active = this;
            BgUI.sprite = Background;
            BtnUI.sprite = Active1;
            BtnTextUI.sprite = Active2;
            LightUI.sprite = Light;
        }
        else
        {
            BtnUI.sprite = Deactive;
        }
        BtnUI.SetNativeSize();
        BtnTextUI.gameObject.SetActive(isActive);
    }
    public void Touch()
    {
        if (!isActive)
        {
            Active.isActive = false;
            isActive = true;
            UpdateApppearance();
        }
        else
        {
            if (TargetScene != "Level1")
            {
                CharacterFormLock.UnLock(CharacterModel.CharacterForm.Ice);
                CharacterFormLock.UnLock(CharacterModel.CharacterForm.Mist);
            }
            Loading.Run(TargetScene);
        }
    }
}
