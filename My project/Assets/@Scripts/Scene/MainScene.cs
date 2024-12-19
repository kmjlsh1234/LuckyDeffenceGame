using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainScene : SceneBase
{
    
    public override void Init()
    {
        base.Init();
        _sceneName = SceneName.MainScene;
        UIManager.Instance.Push(UIType.UIPopupMain);

        
    }
}
