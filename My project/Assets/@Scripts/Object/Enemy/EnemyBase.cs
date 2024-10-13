using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private List<Transform> _movePointerList;
    private CircleCollider2D _collider;
    [SerializeField] private Vector3 _targetPos;

    private int _firstPointerNum;
    private int _targetPointerNum;
    private int _lastPointerNum;

    [SerializeField] private float _speed = 2f;
    
    private bool isMove;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _movePointerList = MapManager.Instance.CurrentMap.EnemyPointers;

        Init();
    }

    public void Init()
    {
        _firstPointerNum = 0;
        _targetPointerNum = _firstPointerNum + 1;
        _lastPointerNum = _movePointerList.Count;

        isMove = false;
    }

    private void FixedUpdate()
    {
        //TODO : 목적지 설정
        if(!isMove)
        {
            _targetPos = SelectTargetPointer();
            _targetPointerNum++;
            isMove = true;
        }

        if(isMove)
        {
            //TODO : 목적지로 이동
            Move();

            //TODO : 목적지까지 거리 체크
            if (IsArriveTargetPos())
            {
                isMove = false;
            }
        }
    }

    #region ::::Move
    private Vector3 SelectTargetPointer()
    {
        if(_targetPointerNum == _lastPointerNum)
        {
            _targetPointerNum = _firstPointerNum;
        }

        return _movePointerList[_targetPointerNum].position;
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
    }    

    private bool IsArriveTargetPos()
    {
        return (Vector3.Distance(transform.position, _targetPos) < 0.01f) ? true : false;

    }
    #endregion
}
