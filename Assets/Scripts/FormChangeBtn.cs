using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormChangeBtn : MonoBehaviour
{
    public CharacterModel.CharacterForm TargetForm;
    public void Touch()
    {
        if (CharacterFormLock.isLocked(TargetForm))
            return;
        CharacterController.Instance.Form = TargetForm;
        if (TargetForm == CharacterModel.CharacterForm.Ice)
            SndPlayer.Play("ice (" + Random.Range(1, 5) + ")");
        else if (TargetForm == CharacterModel.CharacterForm.Water)
            SndPlayer.Play("water (" + Random.Range(1, 5) + ")");
        else
            SndPlayer.Play("mist (" + Random.Range(1, 5) + ")");
    }
}
