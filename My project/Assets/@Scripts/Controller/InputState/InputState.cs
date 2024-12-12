using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputState<T> 
{
    public void OnInputWait();
    public void OnClickStart(T t);

    public void OnClickFinish(T t);

    public void OnDragStart(T t);

    public void OnDrag(T t);

    public void OnDragFinish(T t);
}
