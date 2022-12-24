using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLoad : MonoBehaviour
{
    public string Scene;
    public void Touch()
    {
        Loading.Run(Scene);
    }
}
