using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class HeroArea : HeroBase
{
    
    public override IEnumerator AttackEnemy()
    {
        //���� �ȿ� ���� �ִٸ�
        if (enemiesInRange.Count > 0)
        {
            foreach (EnemyBase enemy in enemiesInRange)
            {
                enemy.OnDamage(heroData.Damage);
            }

            yield return new WaitForSeconds(heroData.AttackSpeed);
        }

        ChangeHeroState(HeroStatus.IDLE);
    }
}
