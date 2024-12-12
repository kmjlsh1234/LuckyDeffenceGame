using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWaitState : InputState<Vector3>
{
    private InputStateService inputStateService;

    public InputWaitState(InputStateService inputStateService)
    {
        this.inputStateService = inputStateService;
    }

    public void OnInputWait(Vector3 touchPos)
    {
        //Not Use
    }

    //WAIT -> CLICK
    public void OnClick(Vector3 touchPos)
    {
        
    }

    public void OnDrag(Vector3 touchPos)
    {
        //Not Use
    }
}
