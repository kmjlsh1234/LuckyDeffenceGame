using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour, IAttack
{
    //Component_Property
    [SerializeField] protected Animator _anim;
    [SerializeField] protected CircleCollider2D _attackRange;

    protected Pos _currentPos;

    //Data_Property
    [SerializeField] protected HeroData _heroData;

    [SerializeField] protected List<EnemyBase> _enemiesInRange = new List<EnemyBase>();

    protected Coroutine _attackRoutine;

    public void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _attackRange = GetComponent<CircleCollider2D>();
    }

    public void Init(Pos pos)
    {
        

        _heroData = DataManager.Instance.FindHeroDataByPrefabName(this.gameObject.name.Replace("(Clone)", ""));
        _attackRange.radius = _heroData.AttackRange;
        _currentPos = pos;
        _enemiesInRange.Clear();

        _attackRoutine = StartCoroutine(AttackEnemy());
    }

    private void OnDisable()
    {
        if(_attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            if(enemyBase!=null)
            {
                _enemiesInRange.Add(enemyBase);
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                _enemiesInRange.Remove(enemyBase);
            }
        }
    }

    public virtual void OnAttack()
    {

    }

    public virtual IEnumerator AttackEnemy()
    {
        yield return null;
    }

    
    
}
