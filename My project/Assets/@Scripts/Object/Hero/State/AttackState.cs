using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class AttackState : MonoBehaviour, HeroState<HeroBase>
{
    private Coroutine attackRoutine;
    
    //ATTACK -> IDLE
    public void Idle(HeroBase hero)
    {
        hero.currentHeroStatus = HeroStatus.IDLE;
    }

    //ATTACK -> MOVE
    public void Move(HeroBase hero)
    {
        hero.currentHeroStatus = HeroStatus.MOVE;

        //MOVE 관련 처리
    }

    //ATTACK -> ATTACK
    public void Attack(HeroBase hero)
    {

    }
}
