using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static bool[] IsPress = new bool[255];
    public KeyCode bindKeyCode;
    public bool IsTemp = false;
    bool isPress = false;
    public void TouchDown()
    {
        isPress = true; 
    }
    public void TouchUp()
    {
        isPress = false;
    }
    private void Update()
    {
        IsPress[(int)bindKeyCode] = isPress;
        if (isPress && IsTemp) isPress = false;
    }
    private void OnEnable()
    {
        isPress = false;
    }
    private void OnDisable()
    {
        IsPress[(int)bindKeyCode] = false;
        isPress = false;
    }
}
