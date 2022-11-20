using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PostcardPanel : MonoBehaviour
{
    private const int _timeControl = 50;
    private const float _adder = 0.02f;
    private Image _image;
    private PlotController _controller;
    public string PostcardName; 
    public GameObject plotController;
    public Sprite Postcard
    {
        get => _image.sprite;
        set => _image.sprite = value;
    }

    private void Start()
    {
        _image = GetComponent<Image>();
        _controller = plotController.GetComponent<PlotController>();
        _controller.NextScene = PostcardCorotine;
    }

    private IEnumerator PostcardCorotine()
    {
        ResourceRequest request = Resources.LoadAsync<Sprite>(PostcardName);
        
        while (!request.isDone)
            yield return null;

        _image.sprite = request.asset as Sprite;

        PlotController.PlotLock = true;

        for (int i = 0; i < _timeControl; i++)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a + _adder);
            yield return new WaitForSeconds(0.01f);
        }

        while (!Input.GetMouseButtonDown(0))
            yield return null;

        LevelPass.Step = 100;

        PlotController.PlotLock = false;
    }
}
