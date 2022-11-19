using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Text))]
public class LoadingProgress : MonoBehaviour
{
    private Text text;
    private float cache = 0;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        float pro = 0;
        if (Loading.operation != null) pro = Loading.operation.progress;
        if (Loading.finished) pro = 1;
        cache += (pro - cache) / 10;
        text.text = (Mathf.Floor(cache * 1000) / 10).ToString("00.0") + "%";
    }
}
