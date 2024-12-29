using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupMain : UIBase
{
    [SerializeField] private Button singlePlayButton;
    [SerializeField] private Button multiPlayButton;
    [SerializeField] private Button settingButton;
    
    public override void Init()
    {
        base.Init();
        
        AddEvent();
        
    }

    private void AddEvent()
    {
        singlePlayButton.OnClickAsObservable().Subscribe(_ => OnClickSinglePlayButton()).AddTo(gameObject);
        multiPlayButton.OnClickAsObservable().Subscribe(_ => OnClickMultiPlayButton()).AddTo(gameObject);
    }

    private void OnClickSinglePlayButton()
    {
        //나중에 선택한거로 바꾸기
        MapManager.Instance.CurrentMapType = MapType.Map_City;
        SceneManager.LoadScene(SceneName.GameScene.ToString());
    }

    private void OnClickMultiPlayButton()
    {
        //나중에 선택한거로 바꾸기
        MapManager.Instance.CurrentMapType = MapType.Map_City;
        SceneManager.LoadScene(SceneName.GameScene.ToString());
    }
}
