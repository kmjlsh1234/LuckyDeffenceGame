using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    
    public HeroBase CurrentSelectHero;

    public void Init()
    {
        InputManager.Instance.OnClickFinish += OnClickFinish;
        InputManager.Instance.OnDragStart += OnDragStart;
        InputManager.Instance.OnDragFinish += OnDragFinish;
    }

    public void OnDestroy()
    {
        InputManager.Instance.OnClickFinish -= OnClickFinish;
        InputManager.Instance.OnDragStart -= OnDragStart;
        InputManager.Instance.OnDragFinish -= OnDragFinish;
    }

    public void OnDragStart(Vector3 touchPos)
    {
        GameObject go = CameraManager.Instance.RaycastTarget(touchPos, "Hero");
        HeroBase hero = go?.GetComponent<HeroBase>();
        if (hero != null)
        {
            CurrentSelectHero = hero;
        }
    }

    public void OnClickFinish(Vector3 touchPos)
    {
        GameObject go = CameraManager.Instance.RaycastTarget(touchPos, "Hero");

        if (go == null) return;
    }

    public void OnDragFinish(Vector3 touchPos)
    {
        GameObject pos = CameraManager.Instance.RaycastTarget(touchPos, "Pos");
        GameObject anotherHero = CameraManager.Instance.RaycastTarget(touchPos, "Hero");

        if (pos == null) return;

        if (CurrentSelectHero == null) return;

        if (anotherHero != null)
        {
            anotherHero.transform.DOMove(CurrentSelectHero.transform.position, 0.5f);
            CurrentSelectHero.transform.DOMove(pos.transform.position, 0.5f);
        }
        else
        {

            CurrentSelectHero.transform.DOMove(pos.transform.position, 0.5f);
            CurrentSelectHero.Pos.IsEmpty = true;
            CurrentSelectHero.Pos = pos.GetComponent<Pos>();
            CurrentSelectHero.Pos.IsEmpty = false;
        }

        CurrentSelectHero = null;
    }
}
