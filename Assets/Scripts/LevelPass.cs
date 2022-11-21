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
                /**if (Instance.TargetScene != "")
                    Loading.Run(Instance.TargetScene);**/

                step = 0;
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
    public Animator BgAnimator, ArrowAnimator;
    public GameObject Destination;

    private void Awake()
    {
        Instance = this;
        DisplayText.text = step + "/" + Instance.MaxStep;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            CharacterFormLock.UnLock(CharacterModel.CharacterForm.Ice);
            CharacterFormLock.UnLock(CharacterModel.CharacterForm.Mist);
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            CharacterController.Instance.gameObject.transform.position = Destination.transform.position;
            // ÷ÿ÷√∆‰À˚◊¥Ã¨
            CharacterController.Instance.moveController._rigidbody.velocity = Vector3.zero;
            CharacterController.Instance.isHandstand = false;
            TemperatureController.Instance.TemperatureReset();
            CharacterController.Instance.characterFormChanger.Form = CharacterModel.CharacterForm.Water;
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            Step = MaxStep;
        }
    }
}
