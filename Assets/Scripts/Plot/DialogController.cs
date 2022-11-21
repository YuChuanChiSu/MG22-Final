using System;
using System.Collections;
using System.Collections.Generic;
using static DialogModel;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public const float WordSpeed = 0.05f;
    public static DialogController Instance = null;

    public Text Name, Content;
    public GameObject ChoicePrefab;
    public List<Dialog> Dialogs;
    public Action Callback;
    private float spanTime;
    private int wIndex = 0, dIndex = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    public void Launch(List<Dialog> dialogs, Action callback)
    {
        Dialogs = dialogs;
        spanTime = 0; wIndex = 0; dIndex = 0;
        Content.text = "";
        Name.text = dialogs[0].Character;
        Callback = callback;
        if (dialogs[0].AudioFile != "" && dialogs[0].AudioFile != null) 
            SndPlayer.Play("Speak\\" + dialogs[0].Character + "\\" + dialogs[0].AudioFile.Replace("“…Œ ","“…ªÛ"));
    }
    public void Vanish()
    {
        if (GetComponent<Animator>().GetFloat("Speed") < 0)
            Destroy(gameObject);
    }
    public void Terminate()
    {
        GetComponent<Animator>().SetFloat("Speed", -2.0f);
        GetComponent<Animator>().Play("DialogAni", 0, 1.0f);
        Instance = null;
    }
    private void Update()
    {
        if (dIndex >= Dialogs.Count) return;
        if (wIndex < Dialogs[dIndex].Content.Length)
        {
            spanTime += Time.deltaTime;
            if (spanTime > WordSpeed)
            {
                spanTime = 0;
                Content.text += Dialogs[dIndex].Content[wIndex];
                wIndex++;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(wIndex < Dialogs[dIndex].Content.Length)
            {
                Content.text = Dialogs[dIndex].Content;
                wIndex = Dialogs[dIndex].Content.Length;
            }
            else
            {
                dIndex++;
                if (dIndex == Dialogs.Count)
                {
                    Callback();
                }
                else
                {
                    Content.text = "";
                    Name.text = Dialogs[dIndex].Character;
                    wIndex = 0;
                    if (Dialogs[dIndex].AudioFile != "" && Dialogs[dIndex].AudioFile != null)
                        SndPlayer.Play("Speak\\" + Dialogs[dIndex].Character + "\\" + Dialogs[dIndex].AudioFile.Replace("“…Œ ", "“…ªÛ"));
                }
            }
        }
    }
}
