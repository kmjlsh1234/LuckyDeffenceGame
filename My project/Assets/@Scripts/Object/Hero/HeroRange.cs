using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HeroRange : HeroBase, IAttack
{
    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override IEnumerator AttackEnemy()
    {
        while (true)
        {
            if (_enemiesInRange.Count > 0)
            {
                
                if (_enemiesInRange[0] != null)
                {
                    GameObject projectile = null;
                    projectile = PoolManager.Instance.Pop("Arrow", "projectile", this.transform);
                    projectile.transform.DOMove(_enemiesInRange[0].transform.position, _heroData.AttackSpeed)
                        .OnComplete(() => projectile.SetActive(false));
                }
                else
                {
                    _enemiesInRange.RemoveAt(0);
                    continue;
                }
                yield return new WaitForSeconds(_heroData.AttackSpeed);
                _enemiesInRange[0].OnDamage(_heroData.Damage); 
            }
            else
            {
                yield return null;
            }
        }
    }
}
