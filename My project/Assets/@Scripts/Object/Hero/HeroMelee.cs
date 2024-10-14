using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class HeroMelee : HeroBase, IAttack
{
    
    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override IEnumerator AttackEnemy()
    {
        while(true)
        {
            if (_enemiesInRange.Count > 0)
            {
                if (_enemiesInRange[0] != null)
                {
                    _enemiesInRange[0].OnDamage(_heroData.Damage);
                    _anim.SetTrigger("Attack");
                }
                else
                {
                    _enemiesInRange.RemoveAt(0);
                    continue;
                }
                yield return new WaitForSeconds(_heroData.AttackSpeed);
            }
            else
            {
                yield return null;
            }
        }
    }
}
