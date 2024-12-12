using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Enum;
using UniRx;
public class HeroBase : MonoBehaviour
{
    //State_Property
    protected IdleState idleState;
    protected MoveState moveState;
    protected AttackState attackState;

    //Component_Property
    public Animator anim { get; private set; }
    private BoxCollider2D touchArea;
    private CircleCollider2D attackRange;

    //Data_Property
    protected HeroData heroData;
    public HeroStatus currentHeroStatus { get; set; }

    public Pos currentPos { get; set; }
    [SerializeField] protected List<EnemyBase> enemiesInRange = new List<EnemyBase>();
    
    protected Coroutine attackRoutine;

    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        touchArea = gameObject.AddComponent<BoxCollider2D>();

        touchArea.offset = Vector2.up * 0.3f;

        GameObject child = transform.GetChild(0).gameObject;
        attackRange = child.AddComponent<CircleCollider2D>();

        idleState = new IdleState();
        moveState = new MoveState();
        attackState = new AttackState();
    }

    public void Init(Pos pos)
    {
        heroData = DataManager.Instance.FindHeroDataByPrefabName(this.gameObject.name.Replace("(Clone)", ""));
        attackRange.radius = heroData.AttackRange;
        currentPos = pos;
        enemiesInRange.Clear();
    }


    protected void Update()
    {
        //현재 이동 중인지 확인
        if (currentHeroStatus == HeroStatus.MOVE) return;

        if(enemiesInRange.Count == 0 && currentHeroStatus != HeroStatus.IDLE)
        {
            ChangeHeroState(HeroStatus.IDLE);
        }
        if(enemiesInRange.Count > 0 && currentHeroStatus == HeroStatus.IDLE)
        {
            ChangeHeroState(HeroStatus.ATTACK);
        }
    }


    public void ChangeHeroState(HeroStatus status)
    {
        HeroState<HeroBase> heroState = RetrieveState();
        switch (status)
        {
            case HeroStatus.IDLE:
                heroState.Idle(this);
                break;
            case HeroStatus.MOVE:
                heroState.Move(this);
                break;
            case HeroStatus.ATTACK:
                heroState.Attack(this);
                break;
        }
    }

    private HeroState<HeroBase> RetrieveState()
    {
        switch (currentHeroStatus)
        {
            case HeroStatus.IDLE:
                return idleState;
            case HeroStatus.MOVE:
                return moveState;
            case HeroStatus.ATTACK:
                return attackState;
            default:
                return idleState;
        }
    }

    protected void OnDisable()
    {
        if(attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemyBase = other.GetComponent<EnemyBase>();
            if(enemyBase!=null)
            {
                enemiesInRange.Add(enemyBase);
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
                enemiesInRange.Remove(enemyBase);
            }
        }
    }

    public void Attack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }

        attackRoutine = StartCoroutine(AttackEnemy());
    }

    public virtual IEnumerator AttackEnemy()
    {
        yield return null;
    }

    
    
}
