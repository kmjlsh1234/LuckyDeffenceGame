using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour, IAttack
{
    //Component_Property
    [SerializeField] protected Animator _anim;
    [SerializeField] protected BoxCollider2D _touchArea = null;
    [SerializeField] protected CircleCollider2D _attackRange = null;
    [SerializeField] protected Canvas _worldCanvas;
    public Pos Pos { get { return _currentPos ; } set { _currentPos = value; } }
    protected Pos _currentPos;

    //Data_Property
    [SerializeField] protected HeroData _heroData;

    [SerializeField] protected List<EnemyBase> _enemiesInRange = new List<EnemyBase>();

    protected Coroutine _attackRoutine;

    public void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _touchArea = gameObject.AddComponent<BoxCollider2D>();
        _worldCanvas = GetComponentInChildren<Canvas>();

        _touchArea.offset = Vector2.up * 0.3f;

        GameObject child = transform.GetChild(0).gameObject;
        _attackRange = child.AddComponent<CircleCollider2D>();
    }

    public void Init(Pos pos)
    {
        _worldCanvas.gameObject.SetActive(false);
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

    public void WorldCanvasOnOff()
    {
        _worldCanvas.gameObject.SetActive(_worldCanvas.gameObject.activeSelf);
    }

    public virtual void OnAttack()
    {

    }

    public virtual IEnumerator AttackEnemy()
    {
        yield return null;
    }

    
    
}
