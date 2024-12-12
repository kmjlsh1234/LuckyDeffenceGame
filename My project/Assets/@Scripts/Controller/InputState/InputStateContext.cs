using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class InputStateContext
{
    private InputWaitState inputWaitState;
    private ClickStartState clickStartState;
    private ClickFinishState clickFinishState;
    private DragStartState dragStartState;
    private DragState dragState;
    private DragFinishState dragFinishState;

    private InputStatus currentInputStatus;

    public void changeInputState(Vector3 touchPos, InputStatus inputStatus)
    {
        InputState<Vector3> inputState = RetrieveInputState();

        switch (inputStatus)
        {
            case InputStatus.INPUT_WAIT:
                inputState.OnInputWait(); 
                break;
          
            case InputStatus.CLICK_START:
                inputState.OnClickStart(touchPos);
                break;
            case InputStatus.CLICK_FINISH:
                inputState.OnClickFinish(touchPos);
                break;
            case InputStatus.DRAG_START:
                inputState.OnDragStart(touchPos);
                break;
            case InputStatus.DRAG:
                inputState.OnDrag(touchPos);
                break;
            case InputStatus.DRAG_FINISH:
                inputState.OnDragFinish(touchPos);
                break;
            default:
                inputState.OnClickStart(touchPos);
                break;
        }
    }

    private InputState<Vector3> RetrieveInputState()
    {
        switch(currentInputStatus)
        {
            case InputStatus.INPUT_WAIT:
                return inputWaitState;
            case InputStatus.CLICK_START:
                return clickStartState;
            case InputStatus.CLICK_FINISH:
                return clickFinishState;
            case InputStatus.DRAG_START:
                return dragStartState;
            case InputStatus.DRAG:
                return dragState;
            case InputStatus.DRAG_FINISH:
                return dragFinishState;
             default:
                return null;
        }

    }
}
