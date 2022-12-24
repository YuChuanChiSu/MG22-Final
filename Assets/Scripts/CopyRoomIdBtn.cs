using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 复制房间id按钮
/// </summary>
public class CopyRoomIdBtn : MonoBehaviour
{
    public Text CopyTarget;
    public void Touch()
    {
        GUIUtility.systemCopyBuffer = CopyTarget.text;
    }
}
