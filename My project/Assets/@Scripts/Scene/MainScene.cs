using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : SceneBase
{
    
    public override void Init()
    {
        _sceneName = SceneName.MainScene;
        UIManager.Instance.Push(UIType.UIPopupMain);
        base.Init();
    }

}
