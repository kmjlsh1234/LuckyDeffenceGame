using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : SingletonBase<PoolManager>
{
    public Dictionary<string, GameObject> _heroDic = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> _enemyDic = new Dictionary<string, GameObject>();

    public List<GameObject> _heroPool = new List<GameObject>();
    public List<GameObject> _enemyPool = new List<GameObject>();

    public Transform _heroPoolRoot = null;
    public Transform _enemyPoolRoot = null;
    public void Init()
    {
        _heroPoolRoot = new GameObject().transform;
        _enemyPoolRoot = new GameObject().transform;

        _heroPoolRoot.name = "@HeroPool";
        _enemyPoolRoot.name = "@EnemyPool";
    }

    //풀에서 꺼내기
    public GameObject Pop(string key, string poolName, Transform parent = null)
    {
        switch(poolName)
        {
            case "hero":
                GameObject hero = _heroPool.FirstOrDefault(x => !x.activeInHierarchy && x.name == key);
                if(hero == null)
                {
                    hero = ResourceManager.Instance.GetHero(key);
                    GameObject obj = Instantiate(hero);
                    obj.transform.SetParent(_heroPoolRoot);
                    obj.transform.position = parent.position;
                    obj.transform.localRotation = Quaternion.identity;
                    obj.transform.localScale = Vector3.one;
                    obj.SetActive(true);
                    obj.name = key;
                    Push(obj, poolName);
                    return obj;
                }
                else
                {
                    hero.transform.position = parent.position;
                    hero.transform.localPosition = Vector3.zero;
                    hero.transform.localRotation = Quaternion.identity;
                    hero.transform.localScale = Vector3.one;
                    hero.SetActive(true);
                    return hero;
                }
            case "enemy":
                GameObject enemy = _enemyPool.FirstOrDefault(x => !x.activeInHierarchy && x.name == key);
                if (enemy == null)
                {
                    enemy = ResourceManager.Instance.GetEnemy(key);
                    GameObject obj = Instantiate(enemy);
                    obj.transform.SetParent(_enemyPoolRoot);
                    obj.transform.position = parent.position;
                    obj.transform.localRotation = Quaternion.identity;
                    obj.transform.localScale = Vector3.one;
                    obj.SetActive(true);
                    obj.name = key;
                    obj.GetComponent<EnemyBase>().Init();
                    Push(obj, poolName);
                    return obj;
                }
                else
                {
                    enemy.transform.SetParent(_enemyPoolRoot);
                    enemy.transform.position = parent.position;
                    enemy.transform.localRotation = Quaternion.identity;
                    enemy.transform.localScale = Vector3.one;
                    enemy.SetActive(true);
                    enemy.GetComponent<EnemyBase>().Init();
                    return enemy;
                }
        }
        return null;
    }

    public void Push(GameObject go, string poolName)
    {
        switch(poolName)
        {
            case "hero":
                _heroPool.Add(go);
                break;
            case "enemy":
                _enemyPool.Add(go);
                break;
        }
    }
}
