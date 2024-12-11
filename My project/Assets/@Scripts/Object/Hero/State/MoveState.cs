using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class MoveState : MonoBehaviour, HeroState<HeroBase>
{
    //MOVE -> IDLE
    public void Idle(HeroBase hero)
    {
        hero.anim.SetTrigger(HeroStatus.IDLE.ToString());
        hero.ChangeHeroState(HeroStatus.IDLE);
    }

    //MOVE -> MOVE
    public void Move(HeroBase hero)
    {
        return;
    }

    //MOVE -> ATTACK
    public void Attack(HeroBase hero)
    {
        hero.anim.SetTrigger(HeroStatus.ATTACK.ToString());
        hero.ChangeHeroState(HeroStatus.ATTACK);

        //공격 처리
        hero.Attack();
    }
    
}
