using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : PlotEvent
{
    public GameObject linkObj;
    public CharacterModel.CharacterForm unlockForm;

    public IEnumerator Unlock(Action callback)
    {
        CharacterFormLock.UnLock(unlockForm);
        linkObj.SetActive(true);
        callback();
        yield break;
    }
}
