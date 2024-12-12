using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class InputController : MonoBehaviour
{
    
    public HeroBase CurrentSelectHero;
    private InputStateContext inputStateContext;
    public void Init()
    {
        InputManager.Instance.OnClickFinish += OnClickFinish;
        InputManager.Instance.OnDragStart += OnDragStart;
        InputManager.Instance.OnDragFinish += OnDragFinish;

        inputStateContext = new InputStateContext();
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
            anotherHero.GetComponent<HeroBase>().ChangeHeroState(HeroStatus.MOVE);
            CurrentSelectHero.ChangeHeroState(HeroStatus.MOVE);
            anotherHero.transform.DOMove(CurrentSelectHero.transform.position, 0.5f);
            CurrentSelectHero.transform.DOMove(pos.transform.position, 0.5f);
        }
        else
        {
            CurrentSelectHero.ChangeHeroState(HeroStatus.MOVE);

            CurrentSelectHero.transform.DOMove(pos.transform.position, 0.5f);
            CurrentSelectHero.currentPos.IsEmpty = true;
            CurrentSelectHero.currentPos = pos.GetComponent<Pos>();
            CurrentSelectHero.currentPos.IsEmpty = false;
        }

        CurrentSelectHero = null;
    }
}
