using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIController : MonoBehaviour
{
    public GameObject Achievement;
    public void StartGame()
    {
        Loading.Run("LevelSelect");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void EnableAchievementPanel()
        => Achievement.SetActive(true);
}
