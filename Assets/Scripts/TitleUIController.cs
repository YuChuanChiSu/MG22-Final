using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIController : MonoBehaviour
{
    public void StartGame()
    {
        Loading.Run("Level1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
