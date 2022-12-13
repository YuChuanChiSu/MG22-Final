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
        //Debug.Log(bindKeyCode + ": Pressed");
        isPress = true; 
    }
    public void TouchUp()
    {
        //Debug.Log(bindKeyCode + ": Released");
        isPress = false;
    }
    private void Update()
    {
        //if (isPress)
        //Debug.Log(bindKeyCode + ":" + gameObject.name + "_" + isPress);
        IsPress[(int)bindKeyCode] = isPress;
        if (isPress && IsTemp)
        {
            //Debug.Log(bindKeyCode + ": Released");
            isPress = false;
        }
    }
    private void Awake()
    {
        //Debug.Log(bindKeyCode + ": Released");
        isPress = false;
    }
    private void OnDisable()
    {
        //Debug.Log(bindKeyCode + ": Released");
        IsPress[(int)bindKeyCode] = false;
        isPress = false;
    }
}
