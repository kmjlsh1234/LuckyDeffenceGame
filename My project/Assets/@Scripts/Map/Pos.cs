using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pos : MonoBehaviour
{
    private BoxCollider _collider;
    private SpriteRenderer _spriteRenderer;

    public bool IsEmpty { get { return _isEmpty; } set { _isEmpty = value; } }
    private bool _isEmpty = true;

    private Tweener _tweener = null;
    
    public void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        InputManager.Instance.OnDrag += OnDrag;
        InputManager.Instance.OnDragFinish += OnDragFinish;
    }

    public void OnDrag()
    {
        if (_tweener != null) return;
        _tweener = _spriteRenderer.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void OnDragFinish(GameObject go)
    {
        if(_tweener != null)
        {
            _tweener.Kill();
            _tweener = null;
        }
        Color color = _spriteRenderer.color;
        color.a = 0f;
        _spriteRenderer.color = color;
    }

    public void OnDestroy()
    {
        InputManager.Instance.OnDrag -= OnDrag;
        InputManager.Instance.OnDragFinish -= OnDragFinish;
    }
}
