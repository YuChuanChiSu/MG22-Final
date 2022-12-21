using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTime : MonoBehaviour
{
    int hour;
    int minute;
    int second;
    int millisecond;
    // �Ѿ����ѵ�ʱ�� 
    float timeSpend = 0.0f;
    Text text_timeSpend;

    // Start is called before the first frame update
    void Start()
    {
        text_timeSpend = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSpend += Time.deltaTime;

        hour = (int)timeSpend / 3600;
        minute = ((int)timeSpend - hour * 3600) / 60;
        second = (int)timeSpend - hour * 3600 - minute * 60;
        millisecond = (int)((timeSpend - (int)timeSpend) * 1000);
        text_timeSpend.text = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", hour, minute, second, millisecond);
    }
}