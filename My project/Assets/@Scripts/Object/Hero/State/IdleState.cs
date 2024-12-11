using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class IdleState : MonoBehaviour, HeroState<HeroBase>
{
    //IDLE -> IDLE
    public void Idle(HeroBase hero)
    {
        //현재 Idle이면 종료
        if(hero.currentHeroStatus == HeroStatus.IDLE) { return; }

        hero.anim.SetTrigger(HeroStatus.IDLE.ToString());
        hero.currentHeroStatus = HeroStatus.IDLE;
    }

    //IDLE -> MOVE
    public void Move(HeroBase hero)
    {
        hero.anim.SetTrigger(HeroStatus.MOVE.ToString());
        hero.currentHeroStatus = HeroStatus.MOVE;

        //MOVE 처리
    }

    //IDLE -> ATTACK
    public void Attack(HeroBase hero)
    {
        hero.anim.SetTrigger(HeroStatus.ATTACK.ToString());
        hero.currentHeroStatus = HeroStatus.ATTACK;

        //ATTACK처리
        hero.Attack();
    }
}
