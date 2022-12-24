using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class number2 : MonoBehaviour
{

    public Text number;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        number.text = Globle.Death.ToString();
    }
}
