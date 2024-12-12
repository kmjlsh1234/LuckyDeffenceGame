using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Enum;

public class InputStateContext : MonoBehaviour
{
    private InputStateService inputStateService;
    //STATE_PROPERTY
    private InputWaitState inputWaitState;
    private ClickState clickState;
    private DragState dragState;

    private InputStatus currentInputStatus;

    public HeroBase currentHero;
    
    private float timer;

    private void Start()
    {
        inputStateService = new InputStateService(MapManager.Instance.CurrentMap.heroPosMap);
        
        inputWaitState = new InputWaitState(inputStateService);
        clickState = new ClickState(inputStateService);
        dragState = new DragState(inputStateService);

        this.currentInputStatus = InputStatus.INPUT_WAIT;
        this.timer = 0f;
    }

    private void Update()
    {
        //UI 터치 제외
        //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) { return; }

        //터치
        if (Input.GetMouseButtonDown(0) && currentInputStatus == InputStatus.INPUT_WAIT)
        {
            changeInputState(Input.mousePosition, InputStatus.CLICK);
        }

        //터치 중일 때
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
        }

        //클릭 STATE인데 클릭이 길어지면 DRAG로 전환
        if (timer > 0.1f && currentInputStatus == InputStatus.CLICK)
        {
            changeInputState(Input.mousePosition, InputStatus.DRAG);
        }

        
        if (Input.GetMouseButtonUp(0))
        {
            changeInputState(Input.mousePosition, InputStatus.INPUT_WAIT);
            timer = 0;
        }
    }

    public void changeInputState(Vector3 touchPos, InputStatus inputStatus)
    {
        InputState<Vector3> inputState = RetrieveInputState();
        
        switch (inputStatus)
        {
            case InputStatus.INPUT_WAIT:
                inputState.OnInputWait(touchPos); 
                break;
          
            case InputStatus.CLICK:
                inputState.OnClick(touchPos);
                break;

            case InputStatus.DRAG:
                inputState.OnDrag(touchPos);
                break;

            default:
                break;
        }
        currentInputStatus = inputStatus;
    }

    private InputState<Vector3> RetrieveInputState()
    {
        switch(currentInputStatus)
        {
            case InputStatus.INPUT_WAIT:
                return inputWaitState;

            case InputStatus.CLICK:
                return clickState;

            case InputStatus.DRAG:
                return dragState;

             default:
                return null;
        }

    }
}
