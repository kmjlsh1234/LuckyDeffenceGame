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

        //MOVE ó��
    }

    //IDLE -> ATTACK
    public void Attack(HeroBase hero)
    {
        hero.currentHeroStatus = HeroStatus.ATTACK;

        //ATTACKó��
        hero.Attack();
    }
}
