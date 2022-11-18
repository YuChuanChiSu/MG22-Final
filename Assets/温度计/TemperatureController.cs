using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;

/// <summary>
/// 温度控制器
/// </summary>
public class TemperatureController : MonoBehaviour
{
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
    /// 温度上升
    /// </summary>
    public void Up()
    {
        if ((int)_chara.Temperature < (int)Temperature.Boil)
        {
            switch (_chara.Temperature)
            {
                case Temperature.Zero:
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    _chara.Temperature = Temperature.Standard;
                    break;

                case Temperature.Standard:
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(true);
                    _chara.Temperature = Temperature.Boil;
                    break;
            }
            //temperatureText.text = ("温度：" + TemperatureData.temperatureValue);
        }
    }

    /// <summary>
    /// 温度下降
    /// </summary>
    public void Down()
    {
        if ((int)_chara.Temperature > (int)Temperature.Zero)
        {
            switch (_chara.Temperature)
            {
                case Temperature.Boil:
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    _chara.Temperature = Temperature.Standard;
                    break;

                case Temperature.Standard:
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(0).gameObject.SetActive(true);
                    _chara.Temperature = Temperature.Zero;
                    break;
            }
            //temperatureText.text = ("温度：" + TemperatureData.temperatureValue);
        }
    }
}
