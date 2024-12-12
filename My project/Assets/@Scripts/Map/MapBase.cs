using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class MapBase : MonoBehaviour
{
    public MapType _mapType;
    
    [SerializeField] private GameObject heroPosMapParent;
    [SerializeField] private Transform enemyPointerParent;

    public Pos[,] heroPosMap = new Pos[6, 3];
    
    public List<Transform> EnemyPointers { get { return enemyPointers; } }
    private List<Transform> enemyPointers = new List<Transform>();

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
        for (int i=0; i< heroPosMap.GetLength(0); i++)
        {
            for(int j=0; j< heroPosMap.GetLength(1); j++)
            {

                heroPosMap[i, j] = heroPosMapParent.transform.GetChild(count).GetComponent<Pos>();
                heroPosMap[i, j].gameObject.name = $"Pos[{i},{j}]";
                heroPosMap[i, j].gameObject.name = $"Pos_{count}";
                count++;
            }
        }

        //TODO : EnemyPointer 추가하기
        foreach(Transform child in enemyPointerParent)
        {
            enemyPointers.Add(child);
        }
    }

    public Pos FindEmptyPos()
    {
        for (int i = 0; i < heroPosMap.GetLength(0); i++)
        {
            for (int j = 0; j < heroPosMap.GetLength(1); j++)
            {
                if (heroPosMap[i, j].isEmpty)
                    return heroPosMap[i, j];
            }
        }
        return null;
    }

    IEnumerator GenerateEnemy()
    {
        GameObject go = PoolManager.Instance.Pop("Enemy_Normal", "enemy", enemyPointers[0]);
        yield return new WaitForSeconds(1f);

        StartCoroutine(GenerateEnemy());
    }
}

