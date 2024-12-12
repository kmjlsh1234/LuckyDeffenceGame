using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class IdleState : MonoBehaviour, HeroState<HeroBase>
{
    //IDLE -> IDLE
    public void Idle(HeroBase hero)
    {
        
    }

    //IDLE -> MOVE
    public void Move(HeroBase hero)
    {
        hero.anim.SetTrigger(HeroStatus.MOVE.ToString());
        hero.currentHeroStatus = HeroStatus.MOVE;

        //MOVE 贸府
    }

    //IDLE -> ATTACK
    public void Attack(HeroBase hero)
    {
        hero.anim.SetTrigger(HeroStatus.ATTACK.ToString());
        hero.currentHeroStatus = HeroStatus.ATTACK;

        //ATTACK贸府
        hero.Attack();
    }
}
