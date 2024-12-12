using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class InputStateService
{
    public HeroBase currentHero { get; set; }
    private Pos[,] heroPosMap;

    public InputStateService(Pos[,] posMap)
    {
        this.heroPosMap = posMap;
    }

    public void SetCurrentHero(Vector3 touchPos)
    {
        GameObject go = CameraManager.Instance.RaycastTarget(touchPos, TagType.Hero.ToString());
        HeroBase hero = go?.GetComponent<HeroBase>();
        if (hero != null)
        {
            currentHero = hero;
        }
    }

    public void DragFinish(Vector3 touchPos)
    {
        GameObject pos = CameraManager.Instance.RaycastTarget(touchPos, TagType.Pos.ToString());
        GameObject anotherHero = CameraManager.Instance.RaycastTarget(touchPos, TagType.Hero.ToString());

        if (pos == null) return;

        if (currentHero == null) return;

        if (anotherHero != null)
        {
            anotherHero.GetComponent<HeroBase>().ChangeHeroState(HeroStatus.MOVE);
            currentHero.ChangeHeroState(HeroStatus.MOVE);
            anotherHero.transform.DOMove(currentHero.transform.position, 0.5f);
            currentHero.transform.DOMove(pos.transform.position, 0.5f);
        }
        else
        {
            currentHero.ChangeHeroState(HeroStatus.MOVE);

            currentHero.transform.DOMove(pos.transform.position, 0.5f);
            currentHero.currentPos.isEmpty = true;
            currentHero.currentPos = pos.GetComponent<Pos>();
            currentHero.currentPos.isEmpty = false;
        }

        currentHero = null;
    }

    public void EnablePos()
    {
        for (int i = 0; i < heroPosMap.GetLength(0); i++)
        {
            for (int j = 0; j < heroPosMap.GetLength(1); j++)
            {
                heroPosMap[i, j].OnDrag();
            }
        }
    }

    public void DisablePos()
    {
        for (int i = 0; i < heroPosMap.GetLength(0); i++)
        {
            for (int j = 0; j < heroPosMap.GetLength(1); j++)
            {
                heroPosMap[i, j].OnWait();
            }
        }
    }

}
