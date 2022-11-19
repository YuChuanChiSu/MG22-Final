using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;

/// <summary>
/// �¶ȿ�����
/// </summary>
public class TemperatureController : MonoBehaviour
{
    public static TemperatureController Instance; 
    CharacterController _chara;
    private void Start()
    {
        _chara = CharacterController.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
          
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
        }
    }


    /// <summary>
    /// �¶�����
    /// </summary>
    public void Up()
    {
        if ((int)_chara.Temperature < (int)Temperature.Boil)
        {
            switch (_chara.Temperature)
            {
                case Temperature.Zero:
                    if (CharacterFormLock.isLocked(CharacterForm.Water) == false)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(true);
                        _chara.Temperature = Temperature.Standard;
                    }
                    else if (CharacterFormLock.isLocked(CharacterForm.Mist) == false)
                    {
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(true);
                        _chara.Temperature = Temperature.Boil;
                    }
                    break;

                case Temperature.Standard:
                    if (CharacterFormLock.isLocked(CharacterForm.Mist) == false)
                    {
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(2).gameObject.SetActive(true);
                        _chara.Temperature = Temperature.Boil;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// �¶��½�
    /// </summary>
    public void Down()
    {
        if ((int)_chara.Temperature > (int)Temperature.Zero)
        {
            switch (_chara.Temperature)
            {
                case Temperature.Boil:
                    if (CharacterFormLock.isLocked(CharacterForm.Water) == false)
                    {
                        transform.GetChild(2).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(true);
                        _chara.Temperature = Temperature.Standard;
                    }
                    else if (CharacterFormLock.isLocked(CharacterForm.Ice) == false)
                    {
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(0).gameObject.SetActive(true);
                        _chara.Temperature = Temperature.Zero;
                    }
                    break;

                case Temperature.Standard:
                    if (CharacterFormLock.isLocked(CharacterForm.Ice) == false)
                    {
                        transform.GetChild(1).gameObject.SetActive(false);
                        transform.GetChild(0).gameObject.SetActive(true);
                        _chara.Temperature = Temperature.Zero;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 人物死亡后，温度重置为25度
    /// </summary>
    public  void  TemperatureReset()
    {

        _chara.Temperature = Temperature.Standard;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
    }

}
