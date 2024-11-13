using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroArea : HeroBase
{
    
    public override IEnumerator AttackEnemy()
    {
        while (true)
        {
            if (_enemiesInRange.Count > 0)
            {
                foreach(EnemyBase enemy in _enemiesInRange)
                {
                    enemy.OnDamage(_heroData.Damage);
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
