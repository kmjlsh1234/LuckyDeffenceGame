using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class MapBase : MonoBehaviour
{
    public MapType _mapType;
    public GameObject HeroPosMap { get { return _heroPosMapParent; } }
    
    [SerializeField] private GameObject _heroPosMapParent;
    private Pos[,] _heroPosMap = new Pos[6, 3];

    [SerializeField] private Transform _enemyPointerParent;
    public List<Transform> EnemyPointers { get { return _enemyPointers; } }
    private List<Transform> _enemyPointers = new List<Transform>();

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        //TODO : Enemy 생성
        StartCoroutine(GenerateEnemy());
    }

    public virtual void Init()
    {
        int count = 0;
        for (int i=0; i< _heroPosMap.GetLength(0); i++)
        {
            for(int j=0; j< _heroPosMap.GetLength(1); j++)
            {

                _heroPosMap[i, j] = _heroPosMapParent.transform.GetChild(count).GetComponent<Pos>();
                _heroPosMap[i, j].gameObject.name = $"Pos[{i},{j}]";
                _heroPosMap[i, j].gameObject.name = $"Pos_{count}";
                count++;
            }
        }

        //TODO : EnemyPointer 추가하기
        foreach(Transform child in _enemyPointerParent)
        {
            _enemyPointers.Add(child);
        }

        

    }

    public Pos FindEmptyPos()
    {
        for (int i = 0; i < _heroPosMap.GetLength(0); i++)
        {
            for (int j = 0; j < _heroPosMap.GetLength(1); j++)
            {
                if (_heroPosMap[i, j].IsEmpty)
                    return _heroPosMap[i, j];
            }
        }
        return null;
    }

    IEnumerator GenerateEnemy()
    {
        GameObject go = PoolManager.Instance.Pop("Enemy_Normal", "enemy", _enemyPointers[0]);
        yield return new WaitForSeconds(1f);

        StartCoroutine(GenerateEnemy());
    }
}

