using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : SingletonBase<InputManager>
{
    public delegate void OnClickEvent(GameObject go);
    public delegate void OnClickFinishEvent();

    public delegate void OnDragStartEvent(GameObject go);
    public delegate void OnDragEvent();
    public delegate void OnDragFinishEvent(GameObject go);
    
    public OnClickEvent OnClick;
    public OnDragStartEvent OnDragStart;
    public OnDragEvent OnDrag;
    public OnClickFinishEvent OnClickFinish;
    public OnDragFinishEvent OnDragFinish;

    private float _timer;
    private bool _isTouchStart;
    private bool _isDragStart;
    public void Init()
    {
        _timer = 0f;
        _isTouchStart = false;
        _isDragStart = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            _isTouchStart = true;
            GameObject hit = RaycastFromCamera("Hero");
            if(hit != null)
            {
                OnClick?.Invoke(hit);
            }
            
        }

        if(_isTouchStart)
        {
            _timer += Time.deltaTime;
        }
        
        if(_timer >= 0.1f)
        {
            if(!_isDragStart)
            {
                _isDragStart = true;
                GameObject hit = RaycastFromCamera("Hero");
                if(hit != null)
                {
                    GameManager.Instance.CurrentSelectHero = hit.GetComponent<HeroBase>();
                }
                
                OnDragStart?.Invoke(hit);
            }
            OnDrag?.Invoke();
        }    

        if(Input.GetMouseButtonUp(0))
        {
            if (_timer < 0.1f)
            {
                OnClickFinish?.Invoke();
            }
            else
            {
                GameObject hit = RaycastFromCamera("Pos");
                OnDragFinish?.Invoke(hit);
            }
            _timer = 0f;
            _isTouchStart = false;
            _isDragStart = false;
        }
    }

    public GameObject RaycastFromCamera(string type)
    {
        Vector2 mousePos = CameraManager.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

        if (hits.Length >0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag(type))
                    return hit.collider.gameObject;
            }
        }
        Debug.LogError("no object detected");
        return null;
    }
}
