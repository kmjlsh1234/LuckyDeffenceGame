using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class SplashScene : SceneBase
{
    public void Awake()
    {
        SoundManager.Instance.Init();
        ResourceManager.Instance.Init();
        DataManager.Instance.Init();
        SceneManagerEx.Instance.Init();
        UIManager.Instance.Init();
        MapManager.Instance.Init();
        PoolManager.Instance.Init();
    }

    
    public override void Init()
    {
        _sceneName = SceneName.SplashScene;
        base.Init();

        UIManager.Instance.Push(UIType.UIPopupSplash);
    }
}