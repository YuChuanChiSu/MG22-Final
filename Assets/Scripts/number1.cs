using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;



public class number1 : MonoBehaviour
{

    public Text number;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        number.text = Globle.GWaterDeath.ToString();
    }
}

