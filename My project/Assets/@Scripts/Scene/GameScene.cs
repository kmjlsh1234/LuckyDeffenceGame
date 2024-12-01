using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class GameScene : SceneBase
{
    private InputController inputController;

    public override void Init()
    {
        _sceneName = SceneName.GameScene;
        base.Init();
        
        UIManager.Instance.Push(UIType.UIPopupGame);
        MapManager.Instance.GenerateMap();
        GameManager.Instance.StartGame();

        if (inputController == null)
        {
            inputController = gameObject.AddComponent<InputController>();
            inputController.Init();
        }
    }
}
