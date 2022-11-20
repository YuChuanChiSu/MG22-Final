using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
    public static List<GameObject> RegisteredChoiceObj = new List<GameObject>();

    public string PlotTag;
    private void Awake()
    {
        RegisteredChoiceObj.Add(gameObject);
    }
    public void Click()
    {
        Debug.Log("Jump to plot '" + PlotTag + "'");
        if (PlotTag == "")
        {
            if (DialogController.Instance != null)
                DialogController.Instance.Terminate();
        }
        else
        {
            PlotController.Active.ExecutePlot(PlotTag);
        }
        foreach (GameObject choiceObj in RegisteredChoiceObj)
            choiceObj.GetComponent<ChoiceController>().Hide();
        RegisteredChoiceObj.Clear();
    }
    public void Vanish()
    {
        if (GetComponent<Animator>().GetFloat("Speed") < 0)
            Destroy(gameObject);
    }
    public void Hide()
    {
        GetComponent<Animator>().SetFloat("Speed", -2.0f);
        GetComponent<Animator>().Play("ChoiceAni", 0, 1.0f);
    }
}
