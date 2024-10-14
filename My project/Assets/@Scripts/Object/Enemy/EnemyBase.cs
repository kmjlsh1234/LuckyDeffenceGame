using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamage
{
    //Component Property
    private CircleCollider2D _collider;
    private Animator _anim;
    
    //Move Property
    private Coroutine _moveRoutine;
    private Vector3 _targetPos;
    private int _firstPointerNum;
    private int _targetPointerNum;
    private int _lastPointerNum;
    private List<Transform> _movePointerList;

    //Data Property
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private float hp;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _collider = GetComponent<CircleCollider2D>();
        _movePointerList = MapManager.Instance.CurrentMap.EnemyPointers;
        _enemyData = DataManager.Instance.FindEnemyDataByPrefabName(this.gameObject.name.Replace("(Clone)",""));
    }

    public void Init()
    {
        _firstPointerNum = 0;
        _targetPointerNum = _firstPointerNum + 1;
        _lastPointerNum = _movePointerList.Count;
        hp = _enemyData.Health;
        _moveRoutine = StartCoroutine(Move());

        _anim.SetBool("Run", true);
    }

    #region ::::Move
    IEnumerator Move()
    {
        //������ ����
        _targetPos = SelectTargetPointer();
        _targetPointerNum++;

        //�̵�
        while(!IsArriveTargetPos())
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPos, _enemyData.Speed * Time.deltaTime);
            yield return null;
        }

        _moveRoutine = StartCoroutine(Move());
    }

    private Vector3 SelectTargetPointer()
    {
        if (_targetPointerNum == _lastPointerNum)
        {
            _targetPointerNum = _firstPointerNum;
        }

        return _movePointerList[_targetPointerNum].position;
    }

    private bool IsArriveTargetPos()
    {
        return (Vector3.Distance(transform.position, _targetPos) < 0.01f) ? true : false;

    }


    #endregion

    public void OnDamage(float damage)
    {
        //hp ����
        hp -= damage;

        //�׾����� üũ
        if(hp <= 0)
        {
            //TODO : �� óġ ���� �� ��ɵ� �߰�(EX. óġ COUNT ++)
            //TODO : ���ӸŴ����� _enemyData.Money++

            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
                _moveRoutine = null;
            }
            GameManager.Instance.KillCount++;
            GameManager.Instance.CurrentMoney += _enemyData.Money;
            gameObject.SetActive(false);
        }
    }


}
