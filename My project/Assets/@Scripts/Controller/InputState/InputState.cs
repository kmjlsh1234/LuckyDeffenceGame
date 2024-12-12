using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputState<T> 
{
    public void OnInputWait(T t);

    public void OnClick(T t);

    public void OnDrag(T t);
}
