using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPass : MonoBehaviour
{
    public static Action Callback;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Callback();
            gameObject.SetActive(false);
        }   
    }
}
