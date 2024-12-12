using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static Enum;

public class GameScene : SceneBase
{
    private InputStateContext inputStateContext;
    private InputStatus currentInputStatus = InputStatus.INPUT_WAIT;

    public GameScene() 
    {
        _sceneName = SceneName.GameScene;
        UIManager.Instance.Push(UIType.UIPopupGame);
        MapManager.Instance.GenerateMap();
        GameManager.Instance.StartGame();

        if (inputStateContext == null)
        {
            inputStateContext = new InputStateContext();
        }
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) { return; }

        if (Input.GetMouseButtonDown(0) && currentInputStatus == InputStatus.INPUT_WAIT)
        {
            inputStateContext.changeInputState(Input.mousePosition, InputStatus.CLICK_START);
            
        }
    }
}
