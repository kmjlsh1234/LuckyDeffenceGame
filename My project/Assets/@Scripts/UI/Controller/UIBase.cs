using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected Canvas _canvas;
    public virtual void Init(ErrorCode code = ErrorCode.SUCCESS) 
    {
        _canvas = this.GetComponent<Canvas>();
    }

    public virtual void SetCanvasLayer(int order)
    {
        if(_canvas == null)
        {
            _canvas = gameObject.AddComponent<Canvas>();
        }
        _canvas.sortingOrder = order;
    }
}
