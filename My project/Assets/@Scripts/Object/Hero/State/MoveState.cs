using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class MoveState : MonoBehaviour, HeroState<HeroBase>
{
    //MOVE -> IDLE
    public void Idle(HeroBase hero)
    {
        hero.currentHeroStatus = HeroStatus.IDLE;
    }

    //MOVE -> MOVE
    public void Move(HeroBase hero)
    {
        
    }

    //MOVE -> ATTACK
    public void Attack(HeroBase hero)
    {
        hero.currentHeroStatus = HeroStatus.ATTACK;
        hero.Attack();
    }
    
}
