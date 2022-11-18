using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTipAnimationCallback : MonoBehaviour
{
    public void Vanish()
    {
        if (GetComponent<Animator>().GetFloat("Speed") < 0)
            Destroy(gameObject);
    }
}
