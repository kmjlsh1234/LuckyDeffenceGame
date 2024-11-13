using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class GameManager : SingletonBase<GameManager>
{
    public Coroutine TimerRoutine;
    public int CurrentMoney;
    public int NeedGenerateMoney;
    public int KillCount;
    public int WaveCount;
    public float TimerFloat;

    public HeroBase CurrentSelectHero;

    public void Init()
    {
        ResetGameSetting();

        InputManager.Instance.OnClickFinish += OnClickFinish;
        InputManager.Instance.OnDragStart += OnDragStart;
        InputManager.Instance.OnDragFinish += OnDragFinish;
    }

    public void ResetGameSetting()
    {
        CurrentMoney = 0;
        NeedGenerateMoney = 10;
        TimerFloat = 0f;
        KillCount = 0;
        WaveCount = 1;
        if(TimerRoutine!=null)
        {
            StopCoroutine(TimerRoutine);
            TimerRoutine = null;
        }
    }

    public void StartGame()
    {
        TimerRoutine = StartCoroutine(Timer());
    }

    public bool CheckGenerateValid()
    {
        return CurrentMoney >= NeedGenerateMoney;
    }

    public void GenerateHero()
    {
        CurrentMoney -= NeedGenerateMoney;
        NeedGenerateMoney += 2;
    }

    IEnumerator Timer()
    {
        while (true)
        {
            if(TimerFloat % 60 ==0)
            {
                WaveCount++;
            }
            TimerFloat += 1f;
            yield return new WaitForSeconds(1f);
        }
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

        if(anotherHero != null)
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
