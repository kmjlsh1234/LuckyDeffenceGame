using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameScene : SceneBase
{
    private InputStateContext inputStateContext;
    

    public override void Init()
    {
        base.Init();

        _sceneName = SceneName.GameScene;
        UIManager.Instance.Push(UIType.UIPopupGame);
        MapManager.Instance.GenerateMap();
        GameManager.Instance.StartGame();

        inputStateContext = this.AddComponent<InputStateContext>();
    }
}