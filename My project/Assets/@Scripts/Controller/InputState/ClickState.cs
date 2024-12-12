using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickState : MonoBehaviour, InputState<Vector3>
{
    private InputStateService inputStateService;

    public ClickState(InputStateService inputStateService)
    {
        this.inputStateService = inputStateService;
    }

    //CLICK -> WAIT
    public void OnInputWait(Vector3 touchPos)
    {
        inputStateService.SetCurrentHero(touchPos);
    }

    public void OnClick(Vector3 touchPos)
    {
        //Not Use
    }

    //CLICK -> DRAG
    public void OnDrag(Vector3 touchPos)
    {
        inputStateService.SetCurrentHero(touchPos);
        inputStateService.EnablePos();
    }
}
