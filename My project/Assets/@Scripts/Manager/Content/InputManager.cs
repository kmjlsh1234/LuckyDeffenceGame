using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : SingletonBase<InputManager>
{
    public delegate void OnClickEvent(Vector3 touchPos);
    public delegate void OnClickFinishEvent(Vector3 touchPos);

    public delegate void OnDragStartEvent(Vector3 touchPos);
    public delegate void OnDragEvent(Vector3 touchPos);
    public delegate void OnDragFinishEvent(Vector3 touchPos);
    
    
    public OnClickEvent OnClick;
    public OnClickFinishEvent OnClickFinish;

    public OnDragStartEvent OnDragStart;
    public OnDragEvent OnDrag;
    public OnDragFinishEvent OnDragFinish;

    private bool _isDragging = false;
    private float _timer = 0f;
    public void Init()
    {
        _isDragging = false;
        _timer = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 �Է��� �߰�
        {
            Vector3 mousePos = Input.mousePosition;
            OnClick?.Invoke(mousePos);
            Debug.Log("���콺 Ŭ�� ����: ");
        }

        if (Input.GetMouseButton(0))
        {
            _timer += Time.deltaTime;
            Vector3 mousePos = Input.mousePosition;

            if(_timer > 0.1f)
            {
                if (!_isDragging)
                {
                    _isDragging = true;
                    OnDragStart?.Invoke(mousePos);
                    Debug.Log("���콺 �巡�� ����: ");
                }
            }
            

            if (_isDragging)
            {
                OnDrag?.Invoke(mousePos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Input.mousePosition;
            if (_isDragging)
            {
                OnDragFinish?.Invoke(mousePos);
                Debug.Log("���콺 �巡�� ��: ");
            }
            else
            {
                OnClickFinish?.Invoke(mousePos);
                Debug.Log("���콺 Ŭ�� ��: ");
            }
            _isDragging = false;
            _timer = 0f;
        }
    }
}
