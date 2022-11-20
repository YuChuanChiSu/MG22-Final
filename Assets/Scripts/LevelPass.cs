using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPass : MonoBehaviour
{
    public static LevelPass Instance;
    private static long step = 0;
    public static long Step
    {
        get
        {
            return step;
        }
        set
        {
            step = value;
            if (step > Instance.MaxStep)
            {
                Loading.Run(Instance.TargetScene);
            }
            else
            {
                Instance.DisplayText.text = step + "/" + Instance.MaxStep;
            }
        }
    }

    public long MaxStep;
    public string TargetScene;
    public Text DisplayText;
    public Animator BgAnimator;

    private void Awake()
    {
        Instance = this;
        DisplayText.text = step + "/" + Instance.MaxStep;
    }
}
