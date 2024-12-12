using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class DragState : MonoBehaviour, InputState<Vector3>
{
    private InputStateService inputStateService;

    public DragState(InputStateService inputStateService)
    {
        this.inputStateService = inputStateService;
    }

    public void OnInputWait(Vector3 touchPos)
    {
        inputStateService.DragFinish(touchPos);
        inputStateService.DisablePos();
    }

    public void OnClick(Vector3 touchPos)
    {
        //NOT USE
    }

    public void OnDrag(Vector3 touchPos)
    {
        //NOT USE
    }
}
