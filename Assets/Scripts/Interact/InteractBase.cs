using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour
{
    public virtual bool Interact()
    {
        throw new NotImplementedException();
    }
    public virtual bool CanActive()
    {
        return true;
    }
}
