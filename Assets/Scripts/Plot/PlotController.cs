using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static DialogModel;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.Serialization;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEngine.UI.CanvasScaler;

public class PlotController : InteractBase
{
    /// <summary>
    /// 情节锁，为True时禁止其他输入操作。
    /// </summary>
    public static bool PlotLock = false;
    public static PlotController Active = null;
    public bool AutoRun = false;
    private GameObject _dialogPrefab;

    public Func<Action,IEnumerator> NextScene { get; set; }
    public string DialogPrefabName = "DialogPrefab";
    public GameObject DialogPrefab
    {
        get
        {
            if (_dialogPrefab == null)
                _dialogPrefab = Resources.Load<GameObject>("Prefabs\\" + DialogPrefabName);
            return _dialogPrefab;
        }
    }

    [Tooltip("入口点选择器")]
    public PlotSelector plotSelector;
    private PlotUnit[] Plots;
    private void Awake()
    {
        Plots = GetComponents<PlotUnit>();
        if (AutoRun)
            ExecutePlot("Entry");
    }
    public void RunDirectly()
    {
        ExecutePlot("Entry");
    }
    public override bool Interact()
    {
        PlotLock = true;
        for (int i = 0; i < InputController.IsPress.Length; i++)
            InputController.IsPress[i] = false;
        Active = this;
        PlotUnit unit = Plots.First(x => x.PlotTag == plotSelector.Select());
        ExecuteCode(unit.PlotCode);
        Debug.Log(unit.PlotTag + " 已退出，可重复对话：" + unit.ReInteractable);
        return !unit.ReInteractable;
    }
    public void ExecutePlot(string tag)
    {
        PlotLock = true;
        ExecuteCode(Plots.First(x => x.PlotTag == tag).PlotCode, 0);
    }
    public void ExecuteCode(string code, int line = 0)
    {
        code = code.Replace("\r", "");
        code = code.Replace("\nstar\nshow", "\nshow\nstar");
        string[] cmd = code.Split('\r', '\n');
        List<Dialog> dialogs = new List<Dialog>();
        for(int i = line; i < cmd.Length; i++)
        {
            Debug.Log("处理：" + cmd[i]);
            string[] p = cmd[i].Split('|');
            if (p[0] == "action")
            {
                Action action = () =>
                {
                    ExecuteCode(code, i + 1);
                };
                GetComponent<PlotEvent>().StartCoroutine(p[1], action);
                return;
            }
            else if (p[0] == "choice")
            {
                float y = DialogController.Instance.ChoicePrefab.transform.localPosition.y,
                      h = DialogController.Instance.ChoicePrefab.GetComponent<RectTransform>().sizeDelta.y;
                for(int j = 1;j < p.Length; j+= 2)
                {
                    GameObject go = Instantiate(DialogController.Instance.ChoicePrefab, DialogController.Instance.transform);
                    Vector3 pos = go.transform.localPosition;
                    pos.y = y;
                    go.transform.localPosition = pos;
                    go.transform.Find("Title").GetComponent<Text>().text = p[j];
                    go.GetComponent<ChoiceController>().PlotTag = p[j + 1];
                    go.SetActive(true);
                    y += (h - 50);
                }
                return;
            }
            else if (p[0] == "star")
            {
                GetComponent<SpriteRenderer>().sprite = GetComponent<NPCPlotSelector>().WaterSprite;
                LevelPass.Step++;
                CharacterController.Instance.HP -= 20;
                if (CharacterController.Instance.HP <= 0)
                {
                    ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "民胞物与，以水归水...";
                    Globle.GWaterDeath += 1;
                    Debug.Log(Globle.GWaterDeath);
                }
            }
            else if (p[0] == "pass")
            {
                if (DialogController.Instance != null)
                    DialogController.Instance.Terminate();
                StartCoroutine(NextScene(() =>
                {
                    ExecuteCode(code, i + 1);
                }));
                return;
            }
            else if (p[0] == "go")
            {
                if (DialogController.Instance != null)
                    DialogController.Instance.Terminate();
                Loading.Run(p[1]);
                return;
            }
            else if (p[0] == "ending")
            {
                EndingPlotSelector end = GetComponent<EndingPlotSelector>();
                if (DialogController.Instance != null)
                    DialogController.Instance.Terminate();
                if (p[1] == "he")
                {
                    end.HE.SetActive(true);
                    end.BE.SetActive(false);
                    PostcardPanel.instance.PostcardName = "End2";
                }
                else
                {
                    end.HE.SetActive(false);
                    end.BE.SetActive(true);
                    PostcardPanel.instance.PostcardName = "End1";
                }
                EndingPass.Callback = () =>
                {
                    ExecuteCode(code, i + 1);
                };
                return;
            }
            else if (p[0] == "show")
            {
                if (DialogController.Instance == null)
                    Instantiate(DialogPrefab).SetActive(true);
                DialogController.Instance.Launch(dialogs, () =>
                {
                    ExecuteCode(code, i + 1);
                });
                return;
            }
            else if (p[0] != "")
            {
                Dialog dialog = new Dialog
                {
                    Character = p[0],
                    Content = p[1]
                };
                if (p.Length > 2) dialog.AudioFile = p[2];
                dialogs.Add(dialog);
            }
        }
        if (DialogController.Instance != null)
            DialogController.Instance.Terminate();
        PlotLock = false;
        for (int i = 0; i < InputController.IsPress.Length; i++)
            InputController.IsPress[i] = false;
        Active = null;
    }
}

public class PlotEvent : MonoBehaviour
{

}

public class PlotSelector : MonoBehaviour
{
    public virtual string Select()
    {
        throw new NotImplementedException();
    }
}

