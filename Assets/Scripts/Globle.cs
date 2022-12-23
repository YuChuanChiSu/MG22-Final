using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globle : MonoBehaviour
{
 

    public static long Death = 0;
    
    public static long GFireDeath = 0;
    public  static long GOutDeath1 = 0;
    public  static long GOutDeath2 = 0;
    public  static long GSwitch = 0;
    public static long GIceDeath
        => Death - GFireDeath -GOutDeath2 - GWaterDeath;
    public static float Time = 1;
    public static long GWaterDeath = 0;


   
}
