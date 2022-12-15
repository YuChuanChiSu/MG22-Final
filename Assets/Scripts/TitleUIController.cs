using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIController : MonoBehaviour
{
    public void StartGame()
    {
        Loading.Run("LevelSelect");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
