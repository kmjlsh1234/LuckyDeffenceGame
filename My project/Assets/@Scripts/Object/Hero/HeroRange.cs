using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Enum;
using System.Threading.Tasks;
public class HeroRange : HeroBase
{
    public override IEnumerator AttackEnemy()
    {
        //범위 안에 적이 있다면
        if (enemiesInRange[0] != null)
        {
            GameObject projectile = PoolManager.Instance.Pop("Arrow", "projectile", this.transform);
            Vector3 targetPos = enemiesInRange[0].transform.position;
            enemiesInRange[0].OnDamage(heroData.Damage);
            projectile.transform.DOMove(targetPos, heroData.AttackSpeed)
                    .OnComplete(() => projectile.SetActive(false));
            
            
            yield return new WaitForSeconds(heroData.AttackSpeed);
        }

        ChangeHeroState(HeroStatus.IDLE);
    }
}
