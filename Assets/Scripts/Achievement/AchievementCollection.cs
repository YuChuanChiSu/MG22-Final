using GenericToolKit.Mvvm;
using GenericToolKit.Mvvm.Json;
using GenericToolKit.Mvvm.UI;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCollection : ObservablePanel
{
    private readonly IList<AchievementModel> _achievementModels
        = new List<AchievementModel>();
    private readonly string _path = Path.Combine(GlobalConfiguration.JsonPath, "Achievements.txt");
    private readonly IJsonStorage _jsonStorage;

    public AchievementCollection(IJsonStorage jsonStorage)
    {
        _jsonStorage = jsonStorage;

        if (!File.Exists(_path))
        {
            string jsonInfo = Resources.Load<TextAsset>("Achievements").text;
            using (FileStream fs = new FileStream(_path, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    sw.WriteLine(jsonInfo);
        }

        _achievementModels = _jsonStorage.ReadList<AchievementModel>("Achievements");
    }

    public AchievementModel this[AchievementEnum key]
        => _achievementModels[(int)key];

    public IEnumerable<AchievementModel> Achievements
        => _achievementModels;

    public bool IsAwarded(AchievementEnum key)
        => Convert.ToBoolean(_achievementModels[(int)key].Awarded);

    public void MakeAwarded(AchievementEnum key)
        => _achievementModels[(int)key].Awarded = 1;

    public void SetPanelEnable(int index, AchievementModel model)
    {
        GetCompoent<Image>($"Achievement{index}").gameObject.SetActive(model.Awarded == 1);
        GetCompoent<Image>($"Cover{index}").gameObject.SetActive(model.Awarded == 0);
    }

    public void OnApplicationQuit()
    {
        _jsonStorage.Write("Achievements", _achievementModels[0]);
        for (int i = 1; i < _achievementModels.Count; i++)
            _jsonStorage.Append("Achievements", _achievementModels[i]);
    }
}