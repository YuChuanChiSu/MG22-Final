using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���Ʒ���id��ť
/// </summary>
public class CopyRoomIdBtn : MonoBehaviour
{
    public Text CopyTarget;
    public void Touch()
    {
        GUIUtility.systemCopyBuffer = CopyTarget.text;
    }
}
