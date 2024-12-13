using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, HeroState<HeroBase>
{
    //IDLE -> IDLE
    public void Idle(HeroBase hero)
    {
        
    }

    //IDLE -> MOVE
    public void Move(HeroBase hero)
    {
        hero.currentHeroStatus = HeroStatus.MOVE;

        //MOVE 贸府
    }

    //IDLE -> ATTACK
    public void Attack(HeroBase hero)
    {
        hero.currentHeroStatus = HeroStatus.ATTACK;

        //ATTACK贸府
        hero.Attack();
    }
}
