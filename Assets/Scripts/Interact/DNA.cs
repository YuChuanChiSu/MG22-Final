using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : InteractBase
{
    public GameObject linkObj;
    public CharacterModel.CharacterForm unlockForm;

    public override bool Interact()
    {
        CharacterFormLock.UnLock(unlockForm);
        linkObj.SetActive(true);
        return true;
    }
}
