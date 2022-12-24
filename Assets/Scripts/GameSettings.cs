using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("�����ţ�" + gameObject.name);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
        Application.logMessageReceived += Application_logMessageReceived;
    }
    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type != LogType.Error && type != LogType.Assert && type != LogType.Exception)
        {
            Dump("[��Ϣ]" + condition);
            return;
        }
        try
        {
            if (stackTrace == "" || stackTrace == null) stackTrace = new System.Diagnostics.StackTrace().ToString();
            Dump("[����]" + condition + "\n" + stackTrace);
            /**GameObject go = Instantiate(Resources.Load<GameObject>("ErrorReport"));
            go.transform.Find("Report").GetComponent<Text>().text = condition + "\n" + stackTrace;
            go.SetActive(true);**/
        }
        catch
        {

        }
    }
    public static void Dump(string content)
    {
        File.AppendAllText(Application.persistentDataPath + "/" + DateTime.Now.ToString("yy.MM.dd") + ".log", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + " " + content + "\n");
    }
}

