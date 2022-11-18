using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 温度的上升和下降
/// </summary>
public class TemperatureUpAndDown : MonoBehaviour
{
   [Tooltip("显示温度的Text")]
    public  Text temperatureText;


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
         if (TemperatureData.temperatureValue< (int)CharacterModel.Temperature.Boil)
        {
            switch (TemperatureData.temperatureValue)
            {
                case (int)CharacterModel.Temperature.Zero:
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    TemperatureData.temperatureValue = (int)CharacterModel.Temperature.Standard;
                    break;

                case (int)CharacterModel.Temperature.Standard:
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(true);
                    TemperatureData.temperatureValue = (int)CharacterModel.Temperature.Boil;
                    break;
            }
            temperatureText.text = ("温度：" +TemperatureData.temperatureValue);
         }
    }

    /// <summary>
    /// 温度下降
    /// </summary>
    public void Down()
    {
        if (TemperatureData.temperatureValue > (int)CharacterModel.Temperature.Zero)
        {
            switch (TemperatureData.temperatureValue)
            {
                case (int)CharacterModel.Temperature.Boil:
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    TemperatureData.temperatureValue = (int)CharacterModel.Temperature.Standard;
                    break;

                case (int)CharacterModel.Temperature.Standard:
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(0).gameObject.SetActive(true);
                    TemperatureData.temperatureValue = (int)CharacterModel.Temperature.Zero;
                    break;
            }
            temperatureText.text = ("温度：" + TemperatureData.temperatureValue);
        }
    }

  
}
