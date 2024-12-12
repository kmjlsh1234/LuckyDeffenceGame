using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWaitState : MonoBehaviour, InputState<Vector3>
{
    public void OnInputWait()
    {

    }

    public void OnClickFinish(Vector3 touchPos)
    {

    }

    public void OnClickStart(Vector3 touchPos)
    {
        GameObject go = CameraManager.Instance.RaycastTarget(touchPos, "Hero");

        if (go != null)
        {

        }
    }

    public void OnDragStart(Vector3 touchPos)
    {

    }

    public void OnDrag(Vector3 touchPos)
    {

    }

    public void OnDragFinish(Vector3 touchPos)
    {

    }
}
