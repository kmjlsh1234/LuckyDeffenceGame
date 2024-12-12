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

    private bool isDragging = false;
    private float timer = 0f;

    public void Init()
    {
        isDragging = false;
        timer = 0f;
    }

    private void Update()
    {
        //UI 터치 시 RETURN
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) { return; }

        if (Input.GetMouseButtonDown(0)) // 마우스 입력을 추가
        {
            Vector3 mousePos = Input.mousePosition;
            OnClick?.Invoke(mousePos);
            Debug.Log("마우스 클릭 시작: ");
        }

        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            Vector3 mousePos = Input.mousePosition;

            if(timer > 0.1f)
            {
                if (!isDragging)
                {
                    isDragging = true;
                    OnDragStart?.Invoke(mousePos);
                    Debug.Log("마우스 드래그 시작: ");
                }
            }
            

            if (isDragging)
            {
                OnDrag?.Invoke(mousePos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Input.mousePosition;
            if (isDragging)
            {
                OnDragFinish?.Invoke(mousePos);
                Debug.Log("마우스 드래그 끝: ");
            }
            else
            {
                OnClickFinish?.Invoke(mousePos);
                Debug.Log("마우스 클릭 끝: ");
            }
            isDragging = false;
            timer = 0f;
        }
    }
}
