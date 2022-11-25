using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSwitch : MonoBehaviour
{
    public GameObject OtherObject;
    public void Touch()
    {
        gameObject.SetActive(false);
        OtherObject.SetActive(true);
    }
}
