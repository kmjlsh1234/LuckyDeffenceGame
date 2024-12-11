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
        hero.anim.SetTrigger(HeroStatus.IDLE.ToString());
        hero.currentHeroStatus = HeroStatus.IDLE;
    }

    //ATTACK -> MOVE
    public void Move(HeroBase hero)
    {
        hero.anim.SetTrigger(HeroStatus.MOVE.ToString());
        hero.currentHeroStatus = HeroStatus.MOVE;

        //MOVE 包访 贸府
    }

    //ATTACK -> ATTACK
    public void Attack(HeroBase hero)
    {
        hero.anim.SetTrigger(HeroStatus.ATTACK.ToString());
        hero.ChangeHeroState(HeroStatus.ATTACK);

        //ATTACK贸府
        hero.Attack();
    }
}
