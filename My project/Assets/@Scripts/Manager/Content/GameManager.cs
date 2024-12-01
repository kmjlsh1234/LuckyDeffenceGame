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

    public void Init()
    {
        ResetGameSetting();
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
}
