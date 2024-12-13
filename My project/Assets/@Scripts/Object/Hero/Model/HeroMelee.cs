using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMelee : HeroBase
{
    public override IEnumerator AttackEnemy()
    {
        //���� �ȿ� ���� �ִٸ�
        if (enemiesInRange[0] != null)
        {
            //Attack damage
            enemiesInRange[0].OnDamage(heroData.Damage);
            yield return new WaitForSeconds(heroData.AttackSpeed);
        }
        
        ChangeHeroState(HeroStatus.IDLE);
    }
}
